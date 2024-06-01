using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Concretes;
using System.Reflection;
using Security;
using Goverment.AuthApi.Business.Utlilities.Caches.Redis;
using Goverment.AuthApi.Business.Utlilities.Caches;
using Goverment.AuthApi.Business.Utlilities.Caches.InMemory;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;
using Goverment.AuthApi.DataAccess.Repositories.Concretes;

namespace Goverment.AuthApi.Extensions
{
    public static class BusinessRegister
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services,IConfiguration configuration)

        {
            //todo validation down  
            /*services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidatorFactory, ServiceProviderValidatorFactory>();*/

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IUserRoleService, UserRoleManager>();
            services.AddScoped<IUserLoginSecurityRepository, UserLoginRepository>();
            services.AddScoped<IAuthService, AuthManager>();
            //services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ICacheService, InMemoryCacheService>();
            services.AddMemoryCache();
            // services.AddScoped<HttpClient>();
            // services.AddScoped<IMailService, MailManager>();
            // Add services to the container.
            /*            services.AddStackExchangeRedisCache(options =>
                        {
                            options.Configuration = configuration["RedisCacheOptions:Configuration"];
                            options.InstanceName = configuration["RedisCacheOptions:InstanceName"];
                        });*/
            services.AddSecurityServices();
            return services;
        }

    }
}
