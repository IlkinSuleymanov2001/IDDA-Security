using Goverment.AuthApi.DataAccess.Context;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;
using Goverment.AuthApi.DataAccess.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Extensions
{
    public static class DataAccessRegister
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<AuthContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthUsers"));
            });

            

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            return services;
        }
    }
}
