using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.Extensions;
using Goverment.Core.Security.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
namespace Core.Security.JWT;

public class JwtHelper : ITokenHelper
{
	public IConfiguration Configuration { get; }
	private readonly TokenOptions _tokenOptions;
	private  DateTime _accessTokenExpiration;
	private  DateTime _refreshExpireDate;
    public const int defaultRoleIdWhenUserCreated=2;

    public JwtHelper(IConfiguration configuration)
	{
		Configuration = configuration;
		_tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _refreshExpireDate = DateTime.Now.AddHours(24);

    }

	public Token CreateToken(User user, IList<Role> roles )
	{
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
		SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

		JwtSecurityToken jwtAccessToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles,_accessTokenExpiration);
        JwtSecurityToken jwtRefreshToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles, _refreshExpireDate);

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

		string? accessToken = jwtSecurityTokenHandler.WriteToken(jwtAccessToken);
        string? refreshToken = jwtSecurityTokenHandler.WriteToken(jwtRefreshToken);


		return new Token
		{
			AccessToken = new AccessToken(accessToken, _accessTokenExpiration),
			RefreshToken = new RefreshToken(refreshToken,_refreshExpireDate)

		};
	}


	private  JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
												   SigningCredentials signingCredentials,
												   IList<Role> roles, DateTime expireDate)
	{
		JwtSecurityToken jwt = new(
			tokenOptions.Issuer,
			tokenOptions.Audience,
            expires: expireDate,
            notBefore: DateTime.Now,
            claims: SetClaims(user, roles),
			signingCredentials: signingCredentials
		);
		return jwt;
	}


	private IEnumerable<Claim> SetClaims(User user, IList<Role> operationClaims)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }


	

	private IEnumerable<Claim> SetClaimForConfirmToken(User user)
	{
		List<Claim> claims = new();
		claims.AddDefaultRole(defaultRoleIdWhenUserCreated);
		return claims;
	}


    public string CreateConfirmToken(User user)
    {
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = new(
            claims: SetClaimForConfirmToken(user)
        );
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);
        user.ConfirmToken = token;
        return token;
    }

    public void  ConfirmTokenParse(string confirmToken, out string email, out int roleId)
	{
		var jwtHandler = new JwtSecurityTokenHandler();
		var tokenData = jwtHandler.ReadJwtToken(confirmToken);
		roleId = defaultRoleIdWhenUserCreated;
        email = default;
		foreach (var claim in tokenData.Claims)
		{
            if(claim.Type== JwtRegisteredClaimNames.Email) email = claim.Value;
            if (claim.Type == "DefaultRoleId") roleId = Int32.Parse(claim.Value);
		}
	}

    public int  GetUserIdFromToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return default;

        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenData = jwtHandler.ReadJwtToken(token);
        
        foreach (var claim in tokenData.Claims)
           if (claim.Type is ClaimTypes.NameIdentifier) return int.Parse(claim.Value);

        return default;
    }

 
}