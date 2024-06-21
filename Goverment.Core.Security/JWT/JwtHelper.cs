using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.Extensions;
using Goverment.Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
namespace Core.Security.JWT;

public class JwtHelper : ITokenHelper
{
	private readonly TokenOptions _tokenOptions;
	private  DateTime _accessTokenExpiration;
	private  DateTime _refreshExpireDate;
    private readonly IHttpContextAccessor _httpContextAccessor;



    public JwtHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _refreshExpireDate = DateTime.UtcNow.AddHours(24);
        _httpContextAccessor = httpContextAccessor;
    }

    public Tokens CreateTokens(User user, IList<Role> roles )
	{
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
		SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

		var  access = CreateToken(_tokenOptions, user, signingCredentials, roles,_accessTokenExpiration);
        var  refresh = CreateToken(_tokenOptions, user, signingCredentials, roles, _refreshExpireDate);

        JwtSecurityTokenHandler tokenHandler = new();

		return new Tokens
		{
			AccessToken = tokenHandler.WriteToken(access),
			RefreshToken = tokenHandler.WriteToken(refresh)

        };
	}


	private  JwtSecurityToken CreateToken(TokenOptions tokenOptions, User user,
												   SigningCredentials signingCredentials,
												   IList<Role> roles, DateTime expireDate)
	{
		JwtSecurityToken jwt = new(
			tokenOptions.Issuer,
			tokenOptions.Audience,
            expires: expireDate,
            notBefore: DateTime.UtcNow,
            claims: SetClaims(user, roles),
			signingCredentials: signingCredentials
		);
		return jwt;
	}


	private IEnumerable<Claim> SetClaims(User user, IList<Role> operationClaims)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Email);
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }


    public string   GetUsername(string token=null)
    {
        if(token is null) token = GetToken();

        if (string.IsNullOrEmpty(token)) return default;

        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenData = jwtHandler.ReadJwtToken(token);
        
        foreach (var claim in tokenData.Claims)
           if (claim.Type is ClaimTypes.NameIdentifier) return claim.Value;

        return default;
    }

    public User GenerateAndSetOTP(User user)
    {
        
            Random rand = new Random();
            var otp = rand.Next(100000, 999999).ToString(); // Generate a random 6-digit number
            user.OtpCode = otp;
            user.OptCreatedDate = DateTime.UtcNow;
            return user;
    }

    public (bool expire , string username) ParseJwtAndCheckExpireTime(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken parsedToken = tokenHandler.ReadJwtToken(token);

        DateTime? expires = parsedToken.ValidTo;

        return (expires > DateTime.UtcNow, GetUsername(token));
    }

    public  string GetToken()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

        if (authorizationHeader != StringValues.Empty)
        {
            /*if (authorizationHeader.Contains(","))
                authorizationHeader = authorizationHeader.ToList().FirstOrDefault().Split(",");*/

            string? jwtHeader = authorizationHeader.ToList().Where(c => c.Contains("Bearer")).FirstOrDefault();
            return jwtHeader != null ? jwtHeader.Split("Bearer").Last().Trim() : string.Empty;
        }

        return string.Empty; ;
    }

    public string IDToken()
    {
        return  Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    }
}