// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity
{
    // default interface uses key of type string
    public interface IRole : IRole<string>
    {
    }

    public interface IRole<out TKey>
    {
        TKey Id { get; }
        string Name { get; set; }
    }
}