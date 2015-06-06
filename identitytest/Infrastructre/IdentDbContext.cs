using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace identitytest.Infrastructre
{
    public class IdentDbContext:IdentityDbContext<ApplicationUser>
    {
        public IdentDbContext()
            : base("idbconnection")
        {
            {
                Configuration.ProxyCreationEnabled = false;
                Configuration.LazyLoadingEnabled = false;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

           
        }

        public DbSet<identitytest.Entities.Audience>Audience{get;set;}

        public static IdentDbContext Create()
        {
            return new IdentDbContext();
        }

    }
}