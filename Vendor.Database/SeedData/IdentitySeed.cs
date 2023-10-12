using Vendor.Database.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vendor.Database.SeedData
{
    public class IdentitySeed
    {
        public static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<Role> roleManager, ILoggerFactory loggerFactory)
        {
            try
            {
                if (await roleManager.Roles.AnyAsync()) return;

                var roles = new List<Role>
                {
                    new Role{Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN"},
                    new Role{Id = Guid.NewGuid().ToString(), Name = "Vendor", NormalizedName = "VENDOR"},
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                if (await userManager.Users.AnyAsync()) return;

                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Vendor",
                    PhoneNumber = "000000",
                    PhoneNumberConfirmed = false,
                    EmailConfirmed = true,
                    NormalizedUserName = "Vendor",
                    Email = "Vendor@vendor.com",
                    UserName = "Vendor@vendor.com",
                    IsActive = true
                };
                var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Vendor");
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DbContextSeed>();
                logger.LogError(ex.Message);
            }
        }

    }
}
