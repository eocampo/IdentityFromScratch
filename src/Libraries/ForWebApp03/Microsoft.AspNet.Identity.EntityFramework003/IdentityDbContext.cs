using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class IdentityDbContext<TUser, TRole, TKey, TUserRole> : DbContext
        where TUser : IdentityUser<TKey, TUserRole>
        where TRole : IdentityRole<TKey, TUserRole>
        where TUserRole : IdentityUserRole<TKey>        
    {
        public IdentityDbContext()
            : this("DefaultConnection") {
        }

        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) {
            //Database.SetInitializer<IdentityDbContext>(new DropCreateDatabaseIfModelChanges<IdentityDbContext>());
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection) {
            //Database.SetInitializer<IdentityDbContext>(new DropCreateDatabaseIfModelChanges<IdentityDbContext>());
        }

        public IdentityDbContext(DbCompiledModel model)
            : base(model) {
            //Database.SetInitializer<IdentityDbContext>(new DropCreateDatabaseIfModelChanges<IdentityDbContext>());
        }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection) {
            //Database.SetInitializer<IdentityDbContext>(new DropCreateDatabaseIfModelChanges<IdentityDbContext>());
        }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model) {
            //Database.SetInitializer<IdentityDbContext>(new DropCreateDatabaseIfModelChanges<IdentityDbContext>());
        }

        public virtual IDbSet<TUser> Users { get; set; }
        public virtual IDbSet<TRole> Roles { get; set; }

        public bool RequireUniqueEmail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            if (modelBuilder == null) {
                throw new ArgumentNullException("modelBuilder");
            }

            // Needed to ensure subclasses share the same table
            var user = modelBuilder.Entity<TUser>()
                .ToTable("AspNetUsers");
            user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);            
            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

            // CONSIDER: u.Email is Required if set on options?
            user.Property(u => u.Email).HasMaxLength(256);

            modelBuilder.Entity<TUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("AspNetUserRoles");

            var role = modelBuilder.Entity<TRole>()
                .ToTable("AspNetRoles");
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }

        /// <summary>
        ///     Validates that UserNames are unique and case insenstive
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
            IDictionary<object, object> items) {
            if (entityEntry != null && entityEntry.State == EntityState.Added) {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as TUser;
                //check for uniqueness of user name and email
                if (user != null) {
                    if (Users.Any(u => String.Equals(u.UserName, user.UserName))) {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "User name {0} is already taken.", user.UserName)));
                    }
                    if (RequireUniqueEmail && Users.Any(u => String.Equals(u.Email, user.Email))) {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "Email {0} is already taken.", user.Email)));
                    }
                }
                else {
                    var role = entityEntry.Entity as TRole;
                    //check for uniqueness of role name
                    if (role != null && Roles.Any(r => String.Equals(r.Name, role.Name))) {
                        errors.Add(new DbValidationError("Role",
                            String.Format(CultureInfo.CurrentCulture, "Role {0} already exists.", role.Name)));
                    }
                }
                if (errors.Any()) {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
