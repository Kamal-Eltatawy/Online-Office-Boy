using Domain.Const;
using Microsoft.AspNetCore.Identity;

namespace Online_Office_Boy.Seeding
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Employee.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.OfficeBoy.ToString()));
            }

        }
    }
}
