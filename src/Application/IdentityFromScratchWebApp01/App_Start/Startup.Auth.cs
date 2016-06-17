using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using IdentityFromScratchWebApp01.Models;

namespace IdentityFromScratchWebApp01
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app) {
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")                
            });
        }
    }
}