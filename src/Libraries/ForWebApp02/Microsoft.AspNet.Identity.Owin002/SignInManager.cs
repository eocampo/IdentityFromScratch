// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Microsoft.AspNet.Identity.Owin
{
    public class SignInManager
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager) {
            if (userManager == null) {
                throw new ArgumentNullException("userManager");
            }
            if (authenticationManager == null) {
                throw new ArgumentNullException("authenticationManager");
            }
            UserManager = userManager;
            AuthenticationManager = authenticationManager;
        }

        public UserManager UserManager { get; set; }
        public IAuthenticationManager AuthenticationManager { get; set; }
        
        public virtual SignInStatus PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout) {
            if (UserManager == null) {
                return SignInStatus.Failure;
            }
            var user = UserManager.FindByName(userName);
            if (user == null) {
                return SignInStatus.Failure;
            }
            if (UserManager.CheckPassword(user, password)) {
                return SignIn(user, isPersistent);                
            }            
            return SignInStatus.Failure;
        }

        public virtual SignInStatus SignIn(IdentityUser user, bool isPersistent, bool rememberBrowser = false) {
            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.UserName),
            },
            DefaultAuthenticationTypes.ApplicationCookie,
            ClaimTypes.Name, ClaimTypes.Role);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));            
            identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));
            
            AuthenticationManager.SignIn(new AuthenticationProperties {
                IsPersistent = isPersistent
            }, identity);

            return SignInStatus.Success;
        }

        //public virtual ClaimsIdentity CreateUserIdentityAsync(TUser user) {
        //    return UserManager.CreateIdentity(user, AuthenticationType);
        //}
        
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
        }
    }
}
