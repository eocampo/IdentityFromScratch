// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity
{
    // default key as a string type:
    public interface IUser : IUser<string>
    {
    }

    public interface IUser<out TKey>
    {
        TKey Id { get; }
        string UserName { get; set; }
    }
}
