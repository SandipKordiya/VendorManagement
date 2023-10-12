using Vendor.Database;
using Vendor.Database.Identity;
using Vendor.Database.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VendorAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
#if DEBUG
            Console.Title = "VendorAPI";
#endif

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                //try
                //{
                //    var context = services.GetRequiredService<ApplicationDbContext>();
                //    await context.Database.MigrateAsync();
                //    //await DbContextSeed.SeedAsync(context, loggerFactory);

                //    var userManager = services.GetRequiredService<UserManager<User>>();
                //    var roleManager = services.GetRequiredService<RoleManager<Role>>();

                //    var identityContext = services.GetRequiredService<ApplicationDbContext>();
                //    await identityContext.Database.MigrateAsync();
                //    await IdentitySeed.SeedUsersAsync(userManager, roleManager, loggerFactory);
                //}
                //catch (Exception ex)
                //{
                //    var logger = loggerFactory.CreateLogger<Program>();
                //    logger.LogError(ex, "An error occured during migration");
                //}
            }
            Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
#if DEBUG
                    webBuilder.UseUrls("http://0.0.0.0:5500");
#else
                    webBuilder.UseUrls("http://0.0.0.0:5500");
#endif
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (!context.HostingEnvironment.IsDevelopment())
                    {
                        var ctxname = context.HostingEnvironment.EnvironmentName;
                        var preHostConfig = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"appsettings.{ctxname}.json", optional: true, reloadOnChange: true)
                            .Build();
                    }
                });

    }
}
