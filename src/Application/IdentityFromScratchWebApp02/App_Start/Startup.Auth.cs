using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace IdentityFromScratchWebApp02
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app) {
            // https://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin.Security.Cookies/CookieAuthenticationExtensions.cs
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")                
            });
        }
    }
}