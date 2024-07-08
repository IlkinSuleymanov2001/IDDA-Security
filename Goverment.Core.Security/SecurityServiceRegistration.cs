using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, JwtHelper>();
        
        return services;
    }
}