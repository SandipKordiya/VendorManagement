using System.Text;
using Vendor.Database;
using Vendor.Database.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace VendorAPI.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            //var builder = services.AddIdentityCore<User>();

            //builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            //builder.AddEntityFrameworkStores<ApplicationDbContext>();
            ////builder.AddRoleValidator<RoleValidator<Role>>();
            //builder.AddRoleManager<RoleManager<Role>>();
            //builder.AddSignInManager<SignInManager<User>>();
            //builder.AddRoleValidator<RoleValidator<Role>>();

            services.AddIdentityCore<User>()
                        .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>()
                //.AddRoleValidator<RoleValidator<Role>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication
                (options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                       ValidIssuer = config["Token:Issuer"],
                       ValidateIssuer = true,
                       ValidateAudience = false
                   };
               });

            services.AddAuthorization();

            return services;
        }
    }
}