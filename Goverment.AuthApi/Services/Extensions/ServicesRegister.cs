using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Concretes;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Core.Security.JWT;


namespace Goverment.AuthApi.Services.Extensions
{
    public static class ServicesRegister
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)

        {
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidatorFactory, ServiceProviderValidatorFactory>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<ITokenHelper, JwtHelper>();
            services.AddHttpContextAccessor();

            return services;
        }

    }
}
