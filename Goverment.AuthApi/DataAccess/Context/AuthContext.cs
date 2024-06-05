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
				optionsBuilder.UseSqlServer("Data Source=SQL6032.site4now.net;Initial Catalog=db_aa9948_usersdb;User Id=db_aa9948_usersdb_admin;Password=Suleymanov@2001");
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
    }
}
