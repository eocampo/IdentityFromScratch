// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.AspNet.Identity
{
    public class IdentityResult
    {
        private static readonly IdentityResult _success = new IdentityResult(true);

        public IdentityResult(params string[] errors)
            : this((IEnumerable<string>)errors) {
        }

        public IdentityResult(IEnumerable<string> errors) {
            if (errors == null) {
                errors = new[] { "An unknown failure has occured." };
            }
            Succeeded = false;
            Errors = errors;
        }

        protected IdentityResult(bool success) {
            Succeeded = success;
            Errors = new string[0];
        }

        public bool Succeeded { get; private set; }

        public IEnumerable<string> Errors { get; private set; }

        public static IdentityResult Success {
            get { return _success; }
        }

        public static IdentityResult Failed(params string[] errors) {
            return new IdentityResult(errors);
        }
    }
}