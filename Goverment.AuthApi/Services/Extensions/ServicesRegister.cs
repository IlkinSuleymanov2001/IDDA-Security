using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Concretes;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Core.Security.JWT;
using Goverment.AuthApi.Services.Filters.Validation;
using Microsoft.AspNetCore.Mvc;
using Goverment.AuthApi.Services.Concretes;
using Goverment.AuthApi.Services.Filters.Transaction;


namespace Goverment.AuthApi.Services.Extensions
{
    public static class ServicesRegister
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)

        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
                options.Filters.Add(typeof(TransactionAttribute));
            });

            services.AddValidatorsFromAssemblyContaining<Program>().AddFluentValidationAutoValidation();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());




            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<OtpService>();
            services.AddScoped<UserSecurityService>();
           

            return services;
        }

    }
}
