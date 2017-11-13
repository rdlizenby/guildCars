namespace GuildCars.UI.Migrations
{
    using GuildCars.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCarsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCarsDbContext context)
        {
            // Load the user and role managers with our custom models
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));


            // have we loaded roles already?
            if (!roleMgr.RoleExists("admin"))
            {
                roleMgr.Create(new AppRole() { Name = "admin" });
                var user = new AppUser()
                {
                    UserName = "admin",
                    FirstName = "John",
                    LastName = "Jones",
                    Email = "testing@guildCars.com"
                };
                userMgr.Create(user, "testing123");
                userMgr.Update(user);
                userMgr.AddToRole(user.Id, "admin");

            }

            if (!roleMgr.RoleExists("sales"))
            {
                roleMgr.Create(new AppRole() { Name = "sales" });
                var user = new AppUser()
                {
                    UserName = "sales",
                    FirstName = "Smooth",
                    LastName = "Sal",
                    Email = "sales@guildCars.com"
                };
                userMgr.Create(user, "testing123");
                userMgr.AddToRole(user.Id, "sales");
            }
        }
    }   
}
