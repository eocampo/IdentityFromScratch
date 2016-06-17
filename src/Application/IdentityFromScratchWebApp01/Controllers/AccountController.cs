using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using IdentityFromScratchWebApp01.Models;
using System.Security.Claims;

namespace IdentityFromScratchWebApp01.Controllers
{
    [Authorize]
    public class AccountController : Controller
    { 
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]        
        public ActionResult Login(LoginViewModel model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            if (model.Password == "Password1") { 
                var identity = new ClaimsIdentity(new [] {
                    new Claim(ClaimTypes.Name, model.UserName),
                },
                DefaultAuthenticationTypes.ApplicationCookie,
                ClaimTypes.Name, ClaimTypes.Role);

                identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));                

                AuthenticationManager.SignIn(new AuthenticationProperties {
                    IsPersistent = model.RememberMe
                }, identity);
                return RedirectToAction("Index", "Home");
            }
            return View(model);            
        }

        // GET: /Account/LogOff        
        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);            
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }           
    }
}