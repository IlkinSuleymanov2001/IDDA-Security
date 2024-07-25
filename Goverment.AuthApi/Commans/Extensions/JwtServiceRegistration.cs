using Core.Security.Encryption;
using Core.Security.JWT;
using Goverment.Core.CrossCuttingConcers.Resposne.Error;
using Goverment.Core.Security.TIme;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Goverment.AuthApi.Commans.Extensions
{
    public static class JwtServiceRegistration
    {

        public static IServiceCollection AddJwtServices(this IServiceCollection services,
          IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidIssuer = tokenOptions?.Issuer,
                        ValidAudience = tokenOptions?.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions?.SecurityKey),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                            expires != null && expires > Date.UtcNow,

                        RoleClaimType = Config.Roles,
                        NameClaimType = Config.Username
                        
                    };
                    options.SaveToken = true; // Optional: Save the token for further processing if needed
                    options.RequireHttpsMetadata = false; // Adjust according to your HTTPS settings

                });
            return services;

        }
    }
}
