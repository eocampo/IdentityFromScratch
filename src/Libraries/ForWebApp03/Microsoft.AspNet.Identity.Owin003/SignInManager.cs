// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace Microsoft.AspNet.Identity.Owin
{
    public class SignInManager<TUser, TKey> : IDisposable
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public SignInManager(UserManager<TUser, TKey> userManager, IAuthenticationManager authenticationManager) {
            if (userManager == null) {
                throw new ArgumentNullException("userManager");
            }
            if (authenticationManager == null) {
                throw new ArgumentNullException("authenticationManager");
            }
            UserManager = userManager;
            AuthenticationManager = authenticationManager;
        }

        private string _authType;
        
        public string AuthenticationType {
            get { return _authType ?? DefaultAuthenticationTypes.ApplicationCookie; }
            set { _authType = value; }
        }

        public UserManager<TUser, TKey> UserManager { get; set; }

        public IAuthenticationManager AuthenticationManager { get; set; }



        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
        }

        public SignInStatus PasswordSignIn(string userName, string password, bool isPersistent) { //, bool shouldLockout) {
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

        public virtual SignInStatus SignIn(TUser user, bool isPersistent, bool rememberBrowser = false) {
            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.UserName),
            },
            DefaultAuthenticationTypes.ApplicationCookie,
            ClaimTypes.Name, ClaimTypes.Role);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id, CultureInfo.InvariantCulture) ));
            identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));
            
            AuthenticationManager.SignIn(new AuthenticationProperties {
                IsPersistent = isPersistent
            }, identity);

            return SignInStatus.Success;
        }
    }
}
