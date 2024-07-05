using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.Extensions;
using Goverment.Core.Security.JWT;
using Goverment.Core.Security.TIme;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
namespace Core.Security.JWT;


public class JwtHelper : ITokenHelper
{
	private readonly TokenOptions _tokenOptions;
	private System.DateTime _accessTokenExpiration;
	private System.DateTime _refreshExpireDate;
    private readonly IHttpContextAccessor _httpContextAccessor;



    public JwtHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _refreshExpireDate = DateTime.UtcNow.AddMinutes(30);
        _httpContextAccessor = httpContextAccessor;
    }

    public Tokens CreateTokens(User user, IList<Role> roles ,string? OrganizatioName=default)
	{
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
		SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        if(roles.Count<=1) roles.Add(new Role(0, "EMPTY"));

        var  access = CreateToken(_tokenOptions, user, signingCredentials, roles,_accessTokenExpiration, OrganizatioName);
        var  refresh = CreateToken(_tokenOptions, user, signingCredentials, roles, _refreshExpireDate, OrganizatioName);

        JwtSecurityTokenHandler tokenHandler = new();

		return new Tokens
		{
			AccessToken = tokenHandler.WriteToken(access),
			RefreshToken = tokenHandler.WriteToken(refresh)

        };
	}


	private  JwtSecurityToken CreateToken(TokenOptions tokenOptions, User user,
												   SigningCredentials signingCredentials,
												   IList<Role> roles, System.DateTime expireDate,
                                                   string? OrganizatioName = default)
	{

        JwtSecurityToken jwt = new(
			tokenOptions.Issuer,
			tokenOptions.Audience,
            expires: expireDate,
            notBefore: Date.UtcNow,
            claims: SetClaims(user, roles,OrganizatioName),
            signingCredentials: signingCredentials
		);
		return jwt;
	}


	private IEnumerable<Claim> SetClaims(User user, IList<Role> operationClaims, string? OrganizatioName=default)
    {
            List<Claim> claims = new();
        var roleArr = operationClaims.Select(c => c.Name).ToArray();
        claims.AddNameIdentifier(user.Email);
        claims.AddRoles(roleArr);

        claims.AddFullName(user.FullName);
        if (!OrganizatioName.IsNullOrEmpty() && roleArr.Contains("STAFF"))
            claims.AddOrganizationName(OrganizatioName);
       
        return claims;
    }


    public string   GetUsername(string token=null)
    {
        if(token is null) token = GetToken();

        if (string.IsNullOrEmpty(token)) return default;

        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenData = jwtHandler.ReadJwtToken(token);
        
        foreach (var claim in tokenData.Claims)
           if (claim.Type is "sub") return claim.Value;

        return default;
    }

   

    public (bool expire , string username) ParseJwtAndCheckExpireTime(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken parsedToken = tokenHandler.ReadJwtToken(token);

        System.DateTime? expires = parsedToken.ValidTo;

        return ((bool)(expires > Date.UtcNow), GetUsername(token));

    }

    public  string GetToken()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

        if (authorizationHeader != StringValues.Empty)
        {
            string? jwtHeader = authorizationHeader.ToList().Where(c => c.Contains("Bearer")).FirstOrDefault();
            return jwtHeader != null ? jwtHeader.Split("Bearer").Last().Trim() : string.Empty;

        }

        return string.Empty; ;
    }

    public string IDToken()
    {
        return  Guid.NewGuid().ToString();

    }

}