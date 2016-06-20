using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class UserStore : UserStore<IdentityUser>
    {
        /// <summary>
        ///     Default constuctor which uses a new instance of a default EntityyDbContext
        /// </summary>
        public UserStore()
            : this(new IdentityDbContext()) {
            DisposeContext = true;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserStore(DbContext context)
            : base(context) {
        }
    }

    public class UserStore<TUser> :
        UserStore<TUser, IdentityRole, string, IdentityUserRole>,
        IUserStore<TUser> where TUser : IdentityUser
    {
        /// <summary>
        ///     Default constuctor which uses a new instance of a default EntityyDbContext
        /// </summary>
        public UserStore()
            : this(new IdentityDbContext()) {
            DisposeContext = true;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserStore(DbContext context)
            : base(context) {
        }
    }

    public class UserStore<TUser, TRole, TKey, TUserRole> :
        IUserRoleStore<TUser, TKey>
        //IUserPasswordStore<TUser, TKey>,
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey, TUserRole>
        where TRole : IdentityRole<TKey, TUserRole>
        where TUserRole : IdentityUserRole<TKey>, new()        
    {
        private readonly EntityStore<TRole> _roleStore;
        private readonly IDbSet<TUserRole> _userRoles;
        private bool _disposed;
        private EntityStore<TUser> _userStore;

        public UserStore(DbContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }
            Context = context;
            AutoSaveChanges = true;
            _userStore = new EntityStore<TUser>(context);
            _roleStore = new EntityStore<TRole>(context);            
            _userRoles = Context.Set<TUserRole>();
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        ///     If true will call dispose on the DbContext during Dispose
        /// </summary>
        public bool DisposeContext { get; set; }

        /// <summary>
        ///     If true will call SaveChanges after Create/Update/Delete
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        private void SaveChanges() {
            if (AutoSaveChanges) {
                Context.SaveChanges();
            }
        }

        //public IQueryable<TUser> Users {
        //    get { return this.Context.Set<TUser>(); } // _userStore.EntitySet; }
        //}
        public IQueryable<TUser> Users {
            get { return _userStore.EntitySet; }
        }

        public bool VerifyPassword(TUser user, string password) {
            IdentityUser theUser = user as IdentityUser;
            if (theUser != null)
                return Microsoft.AspNet.Identity.Crypto.VerifyHashedPassword(theUser.PasswordHash, password);
            else
                return false;
        }

        public bool SetPassword(TUser user, string password) {
            IdentityUser theUser = user as IdentityUser;
            if (theUser != null) {
                user.PasswordHash = Crypto.HashPassword(password);
                return true;
            }
            return false;
        }

        public void AddToRole(TUser user, string roleName) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName)) {
                throw new ArgumentException("Value cannot be null or empty.", "roleName");
            }
            var roleEntity = _roleStore.DbEntitySet.SingleOrDefault(r => r.Name.ToUpper() == roleName.ToUpper());
            if (roleEntity == null) {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                    "Role {0} does not exist.", roleName));
            }

            var ur = new TUserRole { UserId = user.Id, RoleId = roleEntity.Id };
            _userRoles.Add(ur);
        }

        public void RemoveFromRole(TUser user, string roleName) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName)) {
                throw new ArgumentException("Value cannot be null or empty.", "roleName");
            }
            var roleEntity = _roleStore.DbEntitySet.SingleOrDefault(r => r.Name.ToUpper() == roleName.ToUpper());
            if (roleEntity != null) {
                var roleId = roleEntity.Id;
                var userId = user.Id;
                var userRole = _userRoles.FirstOrDefault(r => roleId.Equals(r.RoleId) && r.UserId.Equals(userId));
                if (userRole != null) {
                    _userRoles.Remove(userRole);
                }
            }
        }

        public IList<string> GetRoles(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            var userId = user.Id;
            var query = from userRole in _userRoles
                        where userRole.UserId.Equals(userId)
                        join role in _roleStore.DbEntitySet on userRole.RoleId equals role.Id
                        select role.Name;
            return query.ToList();
        }

        public bool IsInRole(TUser user, string roleName) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName)) {
                throw new ArgumentException("Value cannot be null or empty.", "roleName");
            }
            var role = _roleStore.DbEntitySet.SingleOrDefault(r => r.Name.ToUpper() == roleName.ToUpper());
            if (role != null) {
                var userId = user.Id;
                var roleId = role.Id;
                return _userRoles.Any(ur => ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId));
            }
            return false;
        }

        public void Create(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            _userStore.Create(user);
            SaveChanges();
        }

        public void Update(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            _userStore.Update(user);
            SaveChanges();
        }

        public void Delete(TUser user) {
            ThrowIfDisposed();
            if (user == null) {
                throw new ArgumentNullException("user");
            }
            _userStore.Delete(user);
            SaveChanges();
        }

        public TUser FindById(TKey userId) {
            ThrowIfDisposed();
            return GetUserAggregate(u => u.Id.Equals(userId));
        }

        public TUser FindByName(string userName) {
            ThrowIfDisposed();
            return GetUserAggregate(u => u.UserName.ToUpper() == userName.ToUpper());
        }

        protected virtual TUser GetUserAggregate(Expression<Func<TUser, bool>> filter) {
            TUser user;
            user = Users.FirstOrDefault(filter);
            
            if (user != null) {                
                EnsureRolesLoaded(user);
            }
            return user;
        }

        private void EnsureRolesLoaded(TUser user) {
            if (!Context.Entry(user).Collection(u => u.Roles).IsLoaded) {
                var userId = user.Id;
                _userRoles.Where(uc => uc.UserId.Equals(userId)).LoadAsync();
                Context.Entry(user).Collection(u => u.Roles).IsLoaded = true;
            }
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
            _userStore = null;
        }        

        /// <summary>
        ///     Dispose the store
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
