using System.Diagnostics;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmViewer.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FilmViewerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FilmViewerDbContext context)
        {
            var roles = new[] {"Admin", "User", "Moderator"};
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            foreach (var role in roles)
            {
                rm.Create(new IdentityRole(role));
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var administrator = new ApplicationUser()
            {
                UserName = "Administrator"
            };

            var identity = userManager.Create(administrator, "Administrator");

            userManager.AddToRole(userManager.FindByName("Administrator").Id, "Admin");

            var moderator = new ApplicationUser()
            {
                UserName = "Moderator"
            };

            var moderatorResult = userManager.Create(moderator, "Moderator");

            userManager.AddToRole(userManager.FindByName("Moderator").Id, "Moderator");


            var user = new ApplicationUser()
            {
                UserName = "ApplicationUser"
            };

            var userResult = userManager.Create(user, "ApplicationUser");

            if (userResult.Succeeded)
            {
                foreach (var error in userResult.Errors)
                {
                    Debug.WriteLine(error);
                }
            }

            userManager.AddToRole(userManager.FindByName("ApplicationUser").Id, "User");

        }
    }
}
