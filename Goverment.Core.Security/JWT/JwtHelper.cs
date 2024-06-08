using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace Core.Security.JWT;

public class JwtHelper : ITokenHelper
{
	private readonly TokenOptions _tokenOptions;
	private  DateTime _accessTokenExpiration;
	private  DateTime _refreshExpireDate;
    public const int UserRoleID=2;

    public JwtHelper(IConfiguration configuration)
	{
		_tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _refreshExpireDate = DateTime.Now.AddHours(24);

    }

	public object CreateToken(User user, IList<Role> roles )
	{
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
		SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

		JwtSecurityToken jwtAccessToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles,_accessTokenExpiration);
        JwtSecurityToken jwtRefreshToken = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles, _refreshExpireDate);

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

		return new
		{
			AccessToken = jwtSecurityTokenHandler.WriteToken(jwtAccessToken),
			RefreshToken = jwtSecurityTokenHandler.WriteToken(jwtRefreshToken)

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
        claims.AddNameIdentifier(user.Email);
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }


    public void  ConfirmTokenParse(string confirmToken, out string email, out int roleId)
	{
		var jwtHandler = new JwtSecurityTokenHandler();
		var tokenData = jwtHandler.ReadJwtToken(confirmToken);
		roleId = UserRoleID;
        email = default;
		foreach (var claim in tokenData.Claims)
		{
            if(claim.Type== JwtRegisteredClaimNames.Email) email = claim.Value;
            if (claim.Type == "UserRoleID") roleId = Int32.Parse(claim.Value);
		}
	}

    public string   GetUserEmail(string token)
    {
        if (string.IsNullOrEmpty(token)) return default;

        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenData = jwtHandler.ReadJwtToken(token);
        
        foreach (var claim in tokenData.Claims)
           if (claim.Type is ClaimTypes.NameIdentifier) return claim.Value;

        return default;
    }

 
}