using Goverment.AuthApi.Business.Abstracts;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Core.Security.JWT;
using Microsoft.AspNetCore.Mvc;
using Goverment.AuthApi.Services.Concretes;
using Goverment.AuthApi.Commans.Filters.Validation;
using Goverment.AuthApi.Services.Http;


namespace Goverment.AuthApi.Commans.Extensions
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
            });

            services.AddValidatorsFromAssemblyContaining<Program>()
                .AddFluentValidationAutoValidation();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpClient();
           /* services.AddHttpClient("Admin", (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri("https://adminapi20240708182629.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", );
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5)); // Set lifetime to 5 minutes*/


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenHelper, JwtHelper>(); 
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<OtpService>();
            services.AddScoped<UserSecurityService>();
            return services;
        }



    }
}
