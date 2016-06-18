using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework
{
    public class IdentityDbContext : DbContext
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

        public virtual IDbSet<IdentityUser> Users { get; set; }
        public virtual IDbSet<IdentityRole> Roles { get; set; }

        /// <summary>
        ///     Maps table names, and sets up relationships between the various user entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            if (modelBuilder == null) {
                throw new ArgumentNullException("modelBuilder");
            }

            // Needed to ensure subclasses share the same table
            var user = modelBuilder.Entity<IdentityUser>()
                .ToTable("AspNetUsers");
            //user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            //user.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            //user.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

            //// CONSIDER: u.Email is Required if set on options?
            //user.Property(u => u.Email).HasMaxLength(256);

            //modelBuilder.Entity<TUserRole>()
            //    .HasKey(r => new { r.UserId, r.RoleId })
            //    .ToTable("AspNetUserRoles");

            //modelBuilder.Entity<TUserLogin>()
            //    .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
            //    .ToTable("AspNetUserLogins");

            //modelBuilder.Entity<TUserClaim>()
            //    .ToTable("AspNetUserClaims");

            var role = modelBuilder.Entity<IdentityRole>()
                .ToTable("AspNetRoles");
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            //role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }
    }
}
