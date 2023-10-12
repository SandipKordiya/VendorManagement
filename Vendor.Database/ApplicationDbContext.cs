using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vendor.Database.Identity;

namespace Vendor.Database
{
    public class ApplicationDbContext: IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<VendorFile> VendorFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                         .HasMany(ur => ur.UserRoles)
                         .WithOne(u => u.User)
                         .HasForeignKey(ur => ur.UserId);
            //.IsRequired();

            builder.Entity<Role>()
              .HasMany(ur => ur.UserRoles)
              .WithOne(u => u.Role)
              .HasForeignKey(ur => ur.RoleId);


        }
    }
}
