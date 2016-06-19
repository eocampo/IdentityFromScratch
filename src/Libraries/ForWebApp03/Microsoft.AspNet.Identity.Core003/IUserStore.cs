// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.
// Note this code is modified for educational purposes, the original code uses AsyncMethods.
// This code is intentionally Sync for simplicity

using System;

namespace Microsoft.AspNet.Identity
{
    // default key as a string type:
    public interface IUserStore<TUser> : IUserStore<TUser, string> where TUser : class, IUser<string>
    {
    }

    public interface IUserStore<TUser, in TKey> : IDisposable where TUser : class, IUser<TKey>
    {
        void Create(TUser user);
        void Update(TUser user);
        void Delete(TUser user);

        TUser FindById(TKey userId);
        TUser FindByName(string userName);
    }
}