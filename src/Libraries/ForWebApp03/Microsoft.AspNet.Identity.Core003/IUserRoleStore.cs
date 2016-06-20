// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserRoleStore<TUser> : IUserRoleStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserRoleStore<TUser, in TKey> : IUserStore<TUser, TKey> where TUser : class, IUser<TKey>
    {
        void AddToRole(TUser user, string roleName);
        void RemoveFromRole(TUser user, string roleName);
        IList<string> GetRoles(TUser user);
        bool IsInRole(TUser user, string roleName);
    }
}