namespace HPSMVC.SecurityMigrations
{
    using HPSMVC.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HPSMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SecurityMigrations";
        }

        protected override void Seed(HPSMVC.Models.ApplicationDbContext context)
        {
            //Create a Role Manager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create Role Admin if it does not exist
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }

            //Create a User Manager
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //Now the Admin user named admin1 with password password
            var adminuser = new ApplicationUser
            {
                UserName = "Admin@HPS.com",
                Email = "Admin@HPS.com"
            };

            //Assign admin user to role
            if (!context.Users.Any(u => u.UserName == "Admin@HPS.com"))
            {
                manager.Create(adminuser, "password");
                manager.AddToRole(adminuser.Id, "Admin");
            }
            
        }
    }
}
