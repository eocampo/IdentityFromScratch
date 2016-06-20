using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class RoleStore<TRole> : RoleStore<TRole, string, IdentityUserRole>, IQueryableRoleStore<TRole>
        where TRole : IdentityRole, new()
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public RoleStore()
            : base(new IdentityDbContext()) {
            DisposeContext = true;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context"></param>
        public RoleStore(DbContext context)
            : base(context) {
        }
    }

    public class RoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
        where TRole : IdentityRole<TKey, TUserRole>, new()
    {
        private bool _disposed;
        private EntityStore<TRole> _roleStore;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public RoleStore(DbContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            Context = context;
            _roleStore = new EntityStore<TRole>(context);
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        public DbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public TRole FindById(TKey roleId) {
            ThrowIfDisposed();
            return _roleStore.GetById(roleId);
        }

        public TRole FindByName(string roleName) {
            ThrowIfDisposed();
            return _roleStore.EntitySet.FirstOrDefault(u => u.Name.ToUpper() == roleName.ToUpper());
        }

        public virtual void Create(TRole role) {
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException("role");
            }
            _roleStore.Create(role);
            Context.SaveChanges();
        }

        public virtual void Delete(TRole role) {
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException("role");
            }
            _roleStore.Delete(role);
            Context.SaveChanges();
        }

        public virtual void Update(TRole role) {
            ThrowIfDisposed();
            if (role == null) {
                throw new ArgumentNullException("role");
            }
            _roleStore.Update(role);
            Context.SaveChanges();
        }

        public IQueryable<TRole> Roles {
            get { return _roleStore.EntitySet; }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
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
            //_roleStore = null;
        }
    }
}
