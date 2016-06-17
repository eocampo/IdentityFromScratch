using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using IdentityFromScratchWebApp01.Models;

namespace IdentityFromScratchWebApp01.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }    
        
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
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
        
        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);            
            return RedirectToAction("Index", "Home");
        }      
    }
}