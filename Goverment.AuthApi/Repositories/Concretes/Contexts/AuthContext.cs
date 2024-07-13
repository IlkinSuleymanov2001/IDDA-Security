using Core.Security.Entities;
using Core.Security.JWT;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.Entities.Audit;
using Goverment.Core.Security.Entities.Interfaces;
using Goverment.Core.Security.TIme;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Goverment.AuthApi.Repositories.Concretes.Contexts
{
    public class AuthContext(
        DbContextOptions options,
        ITokenHelper security,
        IHttpContextAccessor contextAccessor)
        : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLoginSecurity> UserLoginSecurities { get; set; }
        public DbSet<UserResendOtpSecurity> UserResendOtpSecurities { get; set; }
        public DbSet<UserAudit> UserAudits { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            await ToWorkAuditAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private async Task ToWorkAuditAsync()
        {
            var modifiedEntities = ChangeTracker.Entries()
           .Where(e => e.Entity is IAuditEntity && e.State != EntityState.Unchanged && e.State != EntityState.Detached).ToList();

            foreach (var userAudit in modifiedEntities.Select(modifiedEntity => new UserAudit(modifiedEntity, security.GetUsername(), contextAccessor)))
                  await UserAudits.AddAsync(userAudit);
        }

        private void UpdateTimestamps()
        {
            var currentTime = Date.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.Entity)
                {
                    case ICreatedTime createdTimeEntity when entry.State == EntityState.Added:
                        createdTimeEntity.CreatedTime = currentTime;
                        break;

                    case IModifiedTime modifiedTimeEntity when entry.State == EntityState.Modified:
                        modifiedTimeEntity.ModifiedTime = currentTime;
                        break;

                    case IDeletedTime deletedTimeEntity when entry.State == EntityState.Deleted:
                        deletedTimeEntity.DeleteTime = currentTime;
                        deletedTimeEntity.IsDelete = true;
                        entry.State = EntityState.Modified;
                        break;

                    case ISoftDeleted softDeletedEntity when entry.State == EntityState.Deleted:
                        softDeletedEntity.IsDelete = true;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }

    }
}
