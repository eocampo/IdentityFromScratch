// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.
// Note this code is modified for educational purposes, the original code uses AsyncMethods.
// This code is intentionally Sync for simplicity

using System;

namespace Microsoft.AspNet.Identity
{
    // default key as a string type:
    public interface IRoleStore<TRole> : IRoleStore<TRole, string> where TRole : IRole<string>
    {
    }

    public interface IRoleStore<TRole, in TKey> : IDisposable where TRole : IRole<TKey>
    {
        void Create(TRole role);
        void Update(TRole role);
        void Delete(TRole role);

        TRole FindById(TKey roleId);
        TRole FindByName(string roleName);
    }
}