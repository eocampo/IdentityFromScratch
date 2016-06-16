// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin",
        Scope = "namespace", Target = "Microsoft.Owin.Security")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin",
        Scope = "namespace", Target = "Owin")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Owin")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace",
        Target = "Microsoft.Owin.Security")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin",
        Scope = "namespace", Target = "Microsoft.AspNet.Identity.Owin")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "member",
        Target =
            "Microsoft.Owin.Security.AuthenticationManagerExtensions.#GetExternalLoginInfoAsync(Microsoft.Owin.Security.IAuthenticationManager)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "member",
        Target =
            "Microsoft.Owin.Security.AuthenticationManagerExtensions.#GetExternalLoginInfo(Microsoft.Owin.Security.IAuthenticationManager)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "member",
        Target =
            "Microsoft.Owin.Security.AuthenticationManagerExtensions.#GetExternalLoginInfo(Microsoft.Owin.Security.IAuthenticationManager,System.String,System.String)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xsrf",
        Scope = "member",
        Target =
            "Microsoft.Owin.Security.AuthenticationManagerExtensions.#GetExternalLoginInfo(Microsoft.Owin.Security.IAuthenticationManager,System.String,System.String)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "member",
        Target =
            "Microsoft.Owin.Security.AuthenticationManagerExtensions.#GetExternalLoginInfoAsync(Microsoft.Owin.Security.IAuthenticationManager,System.String,System.String)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xsrf",
        Scope = "member",
        Target =
            "Microsoft.Owin.Security.AuthenticationManagerExtensions.#GetExternalLoginInfoAsync(Microsoft.Owin.Security.IAuthenticationManager,System.String,System.String)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member",
        Target = "Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider.#Validate(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "type",
        Target = "Microsoft.AspNet.Identity.Owin.ExternalLoginInfo")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login", Scope = "member",
        Target = "Microsoft.AspNet.Identity.Owin.ExternalLoginInfo.#Login")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Microsoft.AspNet.Identity.Owin.FactoryProvider`1.#OnCreate")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Microsoft.AspNet.Identity.Owin.FactoryProvider`1.#OnDispose")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "Microsoft.AspNet.Identity.Owin.SecurityStampValidator.#OnValidateIdentity`2(System.TimeSpan,System.Func`3<!!0,!!1,System.Threading.Tasks.Task`1<System.Security.Claims.ClaimsIdentity>>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "Microsoft.AspNet.Identity.Owin.SecurityStampValidator.#OnValidateIdentity`3(System.TimeSpan,System.Func`3<!!0,!!1,System.Threading.Tasks.Task`1<System.Security.Claims.ClaimsIdentity>>,System.Func`2<System.Security.Claims.ClaimsIdentity,!!2>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin", Scope = "type",
        Target = "Owin.OwinContextExtensions")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Microsoft.AspNet.Identity.Owin.IdentityFactoryProvider`1.#OnDispose")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target = "Microsoft.AspNet.Identity.Owin.IdentityFactoryProvider`1.#OnCreate")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin", Scope = "type",
        Target = "Microsoft.AspNet.Identity.Owin.OwinContextExtensions")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member",
        Target =
            "Owin.AppBuilderExtensions.#CreatePerOwinContext`1(Owin.IAppBuilder,System.Func`3<Microsoft.AspNet.Identity.Owin.IdentityFactoryOptions`1<!!0>,Microsoft.Owin.IOwinContext,!!0>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin",
        Scope = "member",
        Target =
            "Owin.AppBuilderExtensions.#CreatePerOwinContext`1(Owin.IAppBuilder,System.Func`3<Microsoft.AspNet.Identity.Owin.IdentityFactoryOptions`1<!!0>,Microsoft.Owin.IOwinContext,!!0>)"
        )]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Owin",
        Scope = "member",
        Target = "Owin.AppBuilderExtensions.#CreatePerOwinContext`1(Owin.IAppBuilder,System.Func`1<!!0>)")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Scope = "member",
        Target = "Microsoft.AspNet.Identity.Owin.IdentityFactoryMiddleware`2.#.ctor(Microsoft.Owin.OwinMiddleware,!1)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "login", Scope = "member", Target = "Microsoft.AspNet.Identity.Owin.SignInManager`2.#ExternalSignInAsync(Microsoft.AspNet.Identity.Owin.ExternalLoginInfo,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Microsoft.AspNet.Identity.Owin.SignInManager`2.#GetVerifiedUserIdAsync()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "login", Scope = "member", Target = "Microsoft.AspNet.Identity.Owin.SignInManagerExtensions.#ExternalSignIn`2(Microsoft.AspNet.Identity.Owin.SignInManager`2<!!0,!!1>,Microsoft.AspNet.Identity.Owin.ExternalLoginInfo,System.Boolean)")]
