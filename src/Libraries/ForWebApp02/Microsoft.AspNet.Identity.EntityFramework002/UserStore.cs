using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class UserStore : IUserStore
    {
        private bool _disposed;

        public UserStore(IdentityDbContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            Context = context;
        }
        
        public IdentityDbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public virtual IdentityUser FindById(string userId) {
            ThrowIfDisposed();
            return this.Context.Users.Find(userId);
            //return this.Context.Users.Where(u => u.Id.Equals(userId)).FirstOrDefault();
        }

        public virtual IdentityUser FindByName(string userName) {
            ThrowIfDisposed();
            return this.Context.Users.Where(u => u.UserName.Equals(userName)).FirstOrDefault();
        }

        public virtual void Create(IdentityUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            this.Context.Users.Add(user);
            this.Context.SaveChanges();            
        }

        public void Update(IdentityUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            IdentityUser oldUser = this.Context.Users.Find(user.Id);
            if (oldUser == null) {
                throw new NullReferenceException("user does not exist in the database.");
            }
            oldUser.UserName = user.UserName;
            this.Context.SaveChanges();            
        }

        public void Delete(IdentityUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            IdentityUser oldUser = this.Context.Users.Find(user.Id);
            if (oldUser == null) {
                throw new NullReferenceException("user does not exist in the database.");
            }
            this.Context.Users.Remove(oldUser);
            this.Context.SaveChanges();
        }

        public bool CheckPassword(IdentityUser user, string password) {
            return "Password1" == password;
        }

        private void ThrowIfDisposed() {
            if (_disposed) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing) {
            if (DisposeContext && disposing && Context != null) {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;            
        }
        
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
