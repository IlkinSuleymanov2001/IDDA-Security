using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Concretes;
using System.Reflection;
using Goverment.AuthApi.Business.Utlilities.Caches.Redis;
using Goverment.AuthApi.Business.Utlilities.Caches;
using Goverment.AuthApi.Business.Utlilities.Caches.InMemory;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;
using Goverment.AuthApi.DataAccess.Repositories.Concretes;
using FluentValidation;
using FluentValidation.AspNetCore;
using  Security;
using Core.Security.JWT;


namespace Goverment.AuthApi.Extensions
{
    public static class BusinessRegister
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services,IConfiguration configuration)

        {

            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidatorFactory, ServiceProviderValidatorFactory>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IUserRoleService, UserRoleManager>();
            services.AddScoped<IUserLoginSecurityRepository, UserLoginRepository>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICacheService, InMemoryCacheService>();
            services.AddMemoryCache();

            return services;
        }

    }
}
