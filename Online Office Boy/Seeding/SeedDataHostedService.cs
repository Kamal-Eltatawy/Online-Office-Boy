using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Online_Office_Boy.Seeding
{

    public class SeedDataHostedService : IHostedService
    {
        private readonly IServiceProvider _services;

        public SeedDataHostedService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("app");

            try
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var userManagerOffice = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await Seeding.DefaultRoles.SeedAsync(roleManager);
                await Seeding.DefaultUsers.SeedOfficeBoyUserAsync(userManagerOffice);
                await Seeding.DefaultUsers.SeedAdminUserAsync(userManager, roleManager);
                await Seeding.DefaultUsers.SeedClaimsForSuperUser(roleManager);

                logger.LogInformation("Data seeded");
                logger.LogInformation("Application Started");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred while seeding data");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
