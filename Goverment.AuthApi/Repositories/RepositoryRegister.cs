﻿using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Repositories
{
    public static class RepositoryRegister
    {
        public static IServiceCollection AddRepos(this IServiceCollection services,
           IConfiguration configuration)
        {

            services.AddDbContext<AuthContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthUsers"));

        });



            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserLoginSecurityRepository, UserLoginRepository>();
            services.AddScoped<IUserOtpSecurityRepository, UserResendOtpSecurityRepository>();
            return services;
        }
    }
}
