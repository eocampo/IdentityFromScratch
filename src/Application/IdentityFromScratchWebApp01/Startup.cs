using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IdentityFromScratchWebApp01.Startup))]
namespace IdentityFromScratchWebApp01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
