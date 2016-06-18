using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using IdentityFromScratchWebApp02.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityFromScratchWebApp02.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private SignInManager _signInManager;
        private UserManager _userManager;

        public AccountController() {
            
        }

        protected void InitializeIdentity() {
            UserStore store = new UserStore(new IdentityDbContext());
            this._userManager = new UserManager(store);
            this._signInManager = new SignInManager(this._userManager, this.AuthenticationManager);
        }

        public SignInManager SignInManager {
            get {
                if (_signInManager == null)
                    InitializeIdentity();
                return _signInManager;
            }
        }

        public UserManager UserManager {
            get {
                if (_userManager == null)
                    InitializeIdentity();
                return _userManager;
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

            SignInStatus result = this.SignInManager.PasswordSignIn(model.UserName, model.Password, model.RememberMe, false);            

            if ( result == SignInStatus.Success) {                
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous] //[ValidateAntiForgeryToken]        
        public ActionResult Register(RegisterViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            IdentityUser user = new IdentityUser();
            user.UserName = model.UserName;
            this.UserManager.Create(user, model.Password);
            return RedirectToAction("Login", "Account");
        }
    }
}