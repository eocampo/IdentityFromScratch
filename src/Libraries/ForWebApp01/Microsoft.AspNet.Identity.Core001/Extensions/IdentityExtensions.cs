﻿using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.AspNet.Identity
{
    /// <summary>
    ///     Extensions making it easier to get the user name/user id claims off of an identity
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        ///     Return the user name using the UserNameClaimType
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetUserName(this IIdentity identity) {
            if (identity == null) {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null) {
                return ci.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            }
            return null;
        }

        /// <summary>
        ///     Return the claim value for the first claim with the specified type if it exists, null otherwise
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType) {
            if (identity == null) {
                throw new ArgumentNullException("identity");
            }
            var claim = identity.FindFirst(claimType);
            return claim != null ? claim.Value : null;
        }
    }
}
