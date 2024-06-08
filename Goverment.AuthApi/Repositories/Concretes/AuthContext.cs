﻿using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Goverment.AuthApi.Repositories.Concretes
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}