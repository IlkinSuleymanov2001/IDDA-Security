using Core.Security.Encryption;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Goverment.AuthApi.Extensions
{
    public static class JwtServiceRegistration
    {

		public static IServiceCollection AddJWTServices(this IServiceCollection services,
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
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey
						(tokenOptions.SecurityKey)
					};
				});
			return services;
		}
	}
}
