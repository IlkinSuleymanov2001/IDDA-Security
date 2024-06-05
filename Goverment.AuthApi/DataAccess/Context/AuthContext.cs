using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Goverment.AuthApi.DataAccess.Context
{
    public class AuthContext : DbContext
    {
        public IConfiguration Configuration { get; set; }

		public DbSet<User> Users { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLoginSecurity> UserLoginSecurities { get; set; }



        public AuthContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer("Server=tcp:ne-az-sql-serv1.database.windows.net,1433;Initial Catalog=debbanvkwvpv65k;" +
                    "Persist Security Info=False;User ID=uxomqm12gwidr9r;Password=3x*$I9e7*2MQ?yVdT7F5nKtZU;MultipleActiveResultSets=False;" +
                    "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
    }
}
