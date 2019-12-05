using EnRouteTicketing.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnRouteTicketing.Startup))]
namespace EnRouteTicketing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserRoles();
        }

        private void CreateUserRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "EnRoute";
                user.Email = "enroute@gmail.com";
                var checkcreate = userManager.Create(user, "enroute#123");

                if (checkcreate.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }


            if (!roleManager.RoleExists("BusService"))
            {
                var role = new IdentityRole();
                role.Name = "BusService";
                roleManager.Create(role);


            }

            if (!roleManager.RoleExists("Commuter"))
            {
                var role = new IdentityRole();
                role.Name = "Commuter";
                roleManager.Create(role);


            }

        }
    }
}
