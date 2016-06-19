// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    /// <summary>
    ///     Default EntityFramework IUser implementation
    /// </summary>
    public class IdentityUser : IdentityUser<string, IdentityUserRole>, IUser
    {
        public IdentityUser() {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityUser(string userName)
            : this() {
            UserName = userName;
        }
    }

    public class IdentityUser<TKey, TRole> : IUser<TKey>
        where TRole : IdentityUserRole<TKey>        
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public IdentityUser() {            
            Roles = new List<TRole>();            
        }

        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     Navigation property for user roles
        /// </summary>
        public virtual ICollection<TRole> Roles { get; private set; }

        public virtual TKey Id { get; set; }
        public virtual string UserName { get; set; }
    }
}
