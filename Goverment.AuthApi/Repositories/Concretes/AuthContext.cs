using Core.Security.Entities;
using Core.Security.JWT;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.Entities.Audit;
using Goverment.Core.Security.Entities.Interfaces;
using Goverment.Core.Security.TIme;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class AuthContext : DbContext
    {
        public IConfiguration Configuration { get; set; }
        private readonly ITokenHelper _security;
        private readonly IHttpContextAccessor _contextAccessor;

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLoginSecurity> UserLoginSecurities { get; set; }
        public DbSet<UserResendOtpSecurity> UserResendOtpSecurities { get; set; }
        public DbSet<UserAudit> UserAudits { get; set; }


       
        public AuthContext(DbContextOptions options, IConfiguration configuration, ITokenHelper security, IHttpContextAccessor contextAccessor) : base(options)
        {
            Configuration = configuration;
            _security = security;
            _contextAccessor = contextAccessor;
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SoftDeleteAndTimeChangeIntercept();
            await ToWorkAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private async Task  ToWorkAudit()
        {

           var modifiedEntities = ChangeTracker.Entries()
          .Where(e => e.Entity is IAuditEntity)
          .ToList();
           
            foreach (var modifiedEntity in modifiedEntities)
            {
                if (modifiedEntity.State == EntityState.Unchanged || modifiedEntity.State == EntityState.Detached) continue;
                switch (modifiedEntity.Entity) 
                {
                    case User:
                        break;
                }
                await UserAudits.AddAsync(new UserAudit(modifiedEntity, _security.GetUsername(),_contextAccessor));
            }
        }
        private void SoftDeleteAndTimeChangeIntercept() 
        {
            var modifiedEntities = ChangeTracker.Entries().Where(c => c.Entity is ICreatedTime ||
           c.Entity is IModifiedTime || c.Entity is IDeletedTime || c.Entity is ISoftDeleted);

            foreach (var modifiedEntity in modifiedEntities)
            {
                var Entity = modifiedEntity.Entity;
                switch (modifiedEntity.State)
                {
                    case EntityState.Added:
                        if (Entity is ICreatedTime)
                            ((ICreatedTime)Entity).CreatedTime = Date.UtcNow;
                        break;
                    case EntityState.Modified:
                        if (Entity is IModifiedTime)
                            ((IModifiedTime)Entity).ModifiedTime = Date.UtcNow;
                        break;
                    case EntityState.Deleted:
                        if (Entity is IDeletedTime)
                            ((IDeletedTime)Entity).DeleteTime = Date.UtcNow;
                        if (Entity is ISoftDeleted)
                            ((ISoftDeleted)Entity).IsDelete = true;
                        modifiedEntity.State = EntityState.Modified;
                        break;

                }
            }
        }
       
    }
}
