namespace HPS.ASP.SecurityMigrations
{
    using HPS.ASP.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HPS.ASP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"SecurityMigrations";
            ContextKey = "HPS.ASP.Models.ApplicationDbContext";
        }

        protected override void Seed(HPS.ASP.Models.ApplicationDbContext context)
        {
            //Create a Role Manager
            var roleManager = new RoleManager<IdentityRole>(new
                                          RoleStore<IdentityRole>(context));
            //Create Role Admin if it does not exist
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }

            //Create Roles
            if (!context.Roles.Any(r => r.Name == "BoardDirector"))
            {
                var roleresult = roleManager.Create(new IdentityRole("BoardDirector"));
            }

            if (!context.Roles.Any(r => r.Name == "Family"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Family"));
            }

            //Create a User Manager
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            //Now the Admin user named admin1 with password password
            var adminuser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@hps.com"
            };

            //Create the manager a manager user
            var BoardDirector = new ApplicationUser
            {
                UserName = "BoardDirector",
                Email = "BoardDirector@hps.com"
            };

            //Assign admin user to role
            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                manager.Create(adminuser, "password");
                manager.AddToRole(adminuser.Id, "Admin");
            }

            //Assign manager user to role
            if (!context.Users.Any(u => u.UserName == "BoardDirector"))
            {
                manager.Create(BoardDirector, "password");
                manager.AddToRole(BoardDirector.Id, "BoardDirector");
            }

            //Assign Family user to role
            if (!context.Users.Any(u => u.UserName == "BoardDirector"))
            {
                manager.Create(BoardDirector, "password");
                manager.AddToRole(BoardDirector.Id, "BoardDirector");
            }


            //Create a few generic users
            for (int i = 1; i <= 4; i++)
            {
                var user = new ApplicationUser
                {
                    UserName = string.Format("user{0}", i.ToString()),
                    Email = string.Format("user{0}", i.ToString())
                };
                if (!context.Users.Any(u => u.UserName == user.UserName))
                    manager.Create(user, "password");
            }
        }
    }
}
