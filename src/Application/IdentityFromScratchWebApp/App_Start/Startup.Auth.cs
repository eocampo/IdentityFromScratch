using System;
using Owin;

namespace IdentityFromScratchWebApp
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app) {
            // app.CreatePerOwinContext(ApplicationDbContext.Create);
        }
    }
}