using GalleryWebApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Data
{
    public class UserDataSeed
    {
        public static async Task SeedDataAsync(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@gmail.com";
            var testEmail = "test@gmail.com";
            var adminRoleName = "Admin";
            var userRoleName = "User";

            if (!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = adminRoleName
                    },
                    new IdentityRole
                    {
                        Name = userRoleName
                    }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            { 
                var users = new List<User>
                {
                    new User
                    {
                        UserName = "Admin",
                        Email = adminEmail
                    },
                    new User
                    {
                        UserName = "Test",
                        Email = testEmail
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "qazwsX123@");
                }

                var admin = await userManager.FindByEmailAsync(adminEmail);
                await userManager.AddToRoleAsync(admin, adminRoleName);

                var test = await userManager.FindByEmailAsync(testEmail);
                await userManager.AddToRoleAsync(test, userRoleName);
            }
        }
    }
}
