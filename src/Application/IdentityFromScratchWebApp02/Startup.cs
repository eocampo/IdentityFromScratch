using Owin;
using Microsoft.Owin;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartup(typeof(IdentityFromScratchWebApp02.Startup))]
namespace IdentityFromScratchWebApp02
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
