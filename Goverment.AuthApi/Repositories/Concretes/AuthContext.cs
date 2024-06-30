using Core.Security.Entities;
using Core.Security.JWT;
using Goverment.Core.Persistance.Repositories;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.Entities.Audit;
using Goverment.Core.Security.TIme.AZ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class AuthContext : DbContext
    {
        public IConfiguration Configuration { get; set; }
        private readonly ITokenHelper _security;

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLoginSecurity> UserLoginSecurities { get; set; }
        public DbSet<UserResendOtpSecurity> UserResendOtpSecurities { get; set; }
        public DbSet<UserAudit> UserAudits { get; set; }


        public override async   Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var modifiedEntities = ChangeTracker.Entries()
           .Where(e => e.Entity is IAuditEntity)
           .ToList();

            foreach (var modifiedEntity in modifiedEntities)
            {
                if (modifiedEntity.State == EntityState.Unchanged || modifiedEntity.State == EntityState.Detached) continue;

                await UserAudits.AddAsync(new UserAudit(modifiedEntity, _security.GetUsername()));
            }
            return  await base.SaveChangesAsync(cancellationToken);
        }
        public AuthContext(DbContextOptions options, IConfiguration configuration, ITokenHelper security) : base(options)
        {
            Configuration = configuration;
            _security = security;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

       
    }
}
