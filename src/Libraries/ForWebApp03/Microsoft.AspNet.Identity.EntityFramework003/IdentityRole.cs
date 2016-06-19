// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    // Default role with a string key
    public class IdentityRole : IdentityRole<string, IdentityUserRole>
    {
        public IdentityRole() {
            Id = Guid.NewGuid().ToString();
        }

        public IdentityRole(string roleName)
            : this() {
            Name = roleName;
        }
    }

    // key type indepedent IdentityRole implementation:
    public class IdentityRole<TKey, TUserRole> : IRole<TKey> where TUserRole : IdentityUserRole<TKey>
    {
        public IdentityRole() {
            Users = new List<TUserRole>();
        }

        /// <summary>
        ///     Navigation property for users in the role
        /// </summary>
        public virtual ICollection<TUserRole> Users { get; private set; }

        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}
