using System;
using System.Web.Mvc;
using System.Web.Routing;
using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(IdentityFromScratchWebApp03.Startup))]
namespace IdentityFromScratchWebApp03
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app) {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //System.Data.Entity.Database.SetInitializer(
            //    new System.Data.Entity.DropCreateDatabaseAlways<IdentityDbContext>());
            System.Data.Entity.Database.SetInitializer(
                new System.Data.Entity.DropCreateDatabaseIfModelChanges<IdentityDbContext>());

            // AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;
            // AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            ConfigureAuth(app);
        }
    }
}
