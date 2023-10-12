using Vendor.Database;
using Vendor.Database.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VendorAPI.Extensions;
using VendorAPI.Services;
using Vendor.Database.Repository;
using Vendor.Database.Repository.RepositoryImpl;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace VendorAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //configure logging
            services.AddLogging(opt =>
            {
                opt.AddSimpleConsole(options => options.TimestampFormat = "[yyyy'-'MM'-'dd'T'HH':'mm':'ss]");
            });


            // Cors request
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    //    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
            services.AddControllers()
                    .AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );


            // Register database context
            services.AddDbContext<ApplicationDbContext>(x =>
                     x.UseSqlServer(_config.GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddHttpClient();

            services.AddIdentityServices(_config);



            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // app.UseHttpsRedirection();


            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}