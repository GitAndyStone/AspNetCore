using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager) {
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<ApplicationUser> userManager) {
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@123.com")
                .SingleOrDefaultAsync();

            if (testAdmin != null) return;
            //if (testAdmin != null)
            //{
            //    await userManager.DeleteAsync(testAdmin);
            //}

            testAdmin = new ApplicationUser
            {
                UserName = "admin@123.com",
                Email = "admin@123.com"
            };
            await userManager.CreateAsync(testAdmin, "P@ssw0rd");
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }
    }
}
