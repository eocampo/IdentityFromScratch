// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Microsoft.AspNet.Identity
{
    /// <summary>
    ///     UserManager for users where the primary key for the User is of type string
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class UserManager<TUser> : UserManager<TUser, string> where TUser : class, IUser<string>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="store"></param>
        public UserManager(IUserStore<TUser> store)
            : base(store) {
        }
    }

    public class UserManager<TUser, TKey> : IDisposable
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        //private readonly Dictionary<string, IUserTokenProvider<TUser, TKey>> _factors =
        //    new Dictionary<string, IUserTokenProvider<TUser, TKey>>();

        //private IClaimsIdentityFactory<TUser, TKey> _claimsFactory;
        private TimeSpan _defaultLockout = TimeSpan.Zero;
        private bool _disposed;
        //private IPasswordHasher _passwordHasher;
        //private IIdentityValidator<string> _passwordValidator;
        //private IIdentityValidator<TUser> _userValidator;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="store">The IUserStore is responsible for commiting changes via the UpdateAsync/CreateAsync methods</param>
        public UserManager(IUserStore<TUser, TKey> store) {
            if (store == null) {
                throw new ArgumentNullException("store");
            }
            Store = store;
            //UserValidator = new UserValidator<TUser, TKey>(this);
            //PasswordValidator = new MinimumLengthValidator(6);
            //PasswordHasher = new PasswordHasher();
            //ClaimsIdentityFactory = new ClaimsIdentityFactory<TUser, TKey>();
        }

        /// <summary>
        ///     Persistence abstraction that the UserManager operates against
        /// </summary>
        protected internal IUserStore<TUser, TKey> Store { get; set; }

        public virtual IQueryable<TUser> Users {
            get {
                var queryableStore = Store as IQueryableUserStore<TUser, TKey>;
                if (queryableStore == null) {
                    throw new NotSupportedException("Store does not implement IQueryableUserStore<TUser>.");
                }
                return queryableStore.Users;
            }
        }

        /// <summary>
        ///     Dispose this object
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public virtual IdentityResult Create(TUser user) {
            ThrowIfDisposed();            
            Store.Create(user);
            return IdentityResult.Success;
        }

        public virtual IdentityResult Update(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            Store.Update(user);
            return IdentityResult.Success;
        }

        public virtual IdentityResult Delete(TUser user) {
            ThrowIfDisposed();
            Store.Delete(user);
            return IdentityResult.Success;
        }

        public virtual TUser FindById(TKey userId) {
            ThrowIfDisposed();
            return Store.FindById(userId);
        }

        public virtual TUser FindByName(string userName) {
            ThrowIfDisposed();
            if (userName == null) {
                throw new ArgumentNullException("userName");
            }
            return Store.FindByName(userName);
        }

        public virtual IdentityResult Create(TUser user, string password) {
            ThrowIfDisposed();            
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (password == null) {
                throw new ArgumentNullException("password");
            }
            var result = Store.SetPassword(user, password);
            if (!result) {
                return IdentityResult.Failed();
            }
            return Create(user);
        }

        public virtual TUser Find(string userName, string password) {
            ThrowIfDisposed();
            var user = FindByName(userName);
            if (user == null) {
                return null;
            }
            return CheckPassword(user, password) ? user : null;
        }

        public virtual bool CheckPassword(TUser user, string password) {
            ThrowIfDisposed();            
            if (user == null) {
                return false;
            }
            return VerifyPassword(user, password);
        }

        protected virtual bool VerifyPassword(TUser user, string password) {
            return Store.VerifyPassword(user, password);            
        }



        public virtual IList<string> GetRoles(TKey userId) {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = FindById(userId);
            if (user == null) {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "UserId not found.",
                    userId));
            }
            return userRoleStore.GetRoles(user);
        }

        private IUserRoleStore<TUser, TKey> GetUserRoleStore() {
            var cast = Store as IUserRoleStore<TUser, TKey>;
            if (cast == null) {
                throw new NotSupportedException("Store does not implement IUserRoleStore<TUser>.");
            }
            return cast;
        }


        private void ThrowIfDisposed() {
            if (_disposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing && !_disposed) {
                Store.Dispose();
                _disposed = true;
            }
        }

    }
}
