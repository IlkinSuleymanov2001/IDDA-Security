using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Concretes;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
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
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddHttpContextAccessor();

            return services;
        }

    }
}
