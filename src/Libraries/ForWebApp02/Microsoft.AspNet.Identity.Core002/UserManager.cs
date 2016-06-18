using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.AspNet.Identity
{
    public class UserManager
    {
        private bool _disposed;

        public UserManager(IUserStore store) {
            if (store == null) {
                throw new ArgumentNullException("store");
            }
            Store = store;            
        }

        protected internal IUserStore Store { get; set; }

        /// <summary>
        ///     Dispose this object
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Create(IdentityUser user, string password) {
            ThrowIfDisposed();
            user.PasswordHash = Crypto.HashPassword(password);
            Store.Create(user);
        }

        public virtual IdentityUser FindById(string userId) {
            ThrowIfDisposed();
            return Store.FindById(userId);
        }

        public virtual IdentityUser FindByName(string userName) {
            ThrowIfDisposed();
            if (userName == null) {
                throw new ArgumentNullException("userName");
            }
            return Store.FindByName(userName);
        }        
        
        public virtual bool CheckPassword(IdentityUser user, string password) {
            ThrowIfDisposed();
            if (user == null) {
                return false;
            }
            return Crypto.VerifyHashedPassword(user.PasswordHash, password);
            //return Store.CheckPassword(user, password);
        }


        private void ThrowIfDisposed() {
            if (_disposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        ///     When disposing, actually dipose the store
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) {
            if (disposing && !_disposed) {
                Store.Dispose();
                _disposed = true;
            }
        }
    }
}
