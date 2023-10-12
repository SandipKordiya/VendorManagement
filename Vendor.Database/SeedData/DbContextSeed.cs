using Vendor.Database.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Vendor.Database.SeedData
{
    public class DbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //if (!context.Roles.Any())
                //{
                //    var rolesData =
                //        File.ReadAllText(path + @"/SeedData/roles.json");

                //    var roles = JsonSerializer.Deserialize<List<Role>>(rolesData);

                //    foreach (var item in roles)
                //    {
                //        var role = new IdentityRole
                //        {
                //            Name = item.Name,
                //            NormalizedName = item.Name.ToUpper()
                //        };
                //        await roleManager.CreateAsync(role);
                //    }
                //}

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
