using Domain.Const;
using Online_Office_Boy.Helper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Online_Office_Boy.Seeding
{
    public static class DefaultUsers
    {
        public static async Task SeedOfficeBoyUserAsync(UserManager<User> userManager)
        {
            var defaultUser = new User()
            {
                Email = "OfficeBoy@gmail.com",
                UserName = "Test",
                EmailConfirmed = true,
                OfficeId=2,
                Name="Employee1",
                PhoneNumber="01111111111",
                ShiftId=1
               
        ,

               
            };
            if (await userManager.FindByEmailAsync(defaultUser.Email) == null)
            {
                await userManager.CreateAsync(defaultUser, "123456");
                await userManager.AddToRoleAsync(defaultUser, Roles.OfficeBoy.ToString());
            }
            var defaultUser2 = new User()
            {
                Email = "OfficeBoy1@gmail.com",
                UserName = "Test1",
                EmailConfirmed = true,
                OfficeId = 2,
                Name = "Employee1",
                PhoneNumber = "01111111111",
                ShiftId = 1

       ,


            };
            if (await userManager.FindByEmailAsync(defaultUser2.Email) == null)
            {
                await userManager.CreateAsync(defaultUser2, "123456");
                await userManager.AddToRoleAsync(defaultUser2, Roles.OfficeBoy.ToString());
            }

        }
        public static async Task SeedAdminUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new User()
            {
                Email = "Admin@gmail.com",
                UserName = "Test2",
                EmailConfirmed = true,
                OfficeId = 1,
                Name = "Employee2",
                PhoneNumber = "01111111112",
            };

            if (await userManager.FindByEmailAsync(defaultUser.Email) == null)
            {
                await userManager.CreateAsync(defaultUser, "123456");
                await userManager.AddToRolesAsync(defaultUser, new List<string>
        {
            Roles.Admin.ToString(),
            Roles.Employee.ToString(),
            Roles.OfficeBoy.ToString()
        });

                Console.WriteLine(" Admin User seeded successfully.");

                var adminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());
                if (adminRole != null)
                {
                    await roleManager.SeedClaimsForSuperUser();
                    Console.WriteLine("Claims seeded for Super Admin User.");
                }
                else
                {
                    Console.WriteLine(" Admin role not found.");
                }
            }
        }

        public static async Task SeedClaimsForSuperUser(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            if (adminRole != null)
            {
                await roleManager.AddAllPermissionClaims(adminRole);
            }
        }

    }
}
