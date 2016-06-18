using System;
using System.Web.Mvc;
using System.Web.Routing;
using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(IdentityFromScratchWebApp03.Startup))]
namespace IdentityFromScratchWebApp03
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app) {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ConfigureAuth(app);
        }
    }
}
