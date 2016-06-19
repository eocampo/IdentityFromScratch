// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity.EntityFramework
{
    // default string key implementation:
    public class IdentityUserRole : IdentityUserRole<string>
    {
    }

    public class IdentityUserRole<TKey>
    {
        public virtual TKey UserId { get; set; }
        public virtual TKey RoleId { get; set; }
    }
}