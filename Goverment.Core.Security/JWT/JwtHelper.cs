using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;
using System.Text;
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

    public Tokens CreateTokens(User user, IList<Role> roles ,AddtionalParam? param=default)
	{
        _accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
		SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        if(roles.Count<=1) roles.Add(new Role(0, "EMPTY"));

        var  access = CreateToken(_tokenOptions, user, signingCredentials, roles,_accessTokenExpiration, param);
        var  refresh = CreateToken(_tokenOptions, user, signingCredentials, roles, _refreshExpireDate, param);

        JwtSecurityTokenHandler tokenHandler = new();

		return new Tokens
		{
			AccessToken = tokenHandler.WriteToken(access),
			RefreshToken = tokenHandler.WriteToken(refresh)
        };
	}


	private  JwtSecurityToken CreateToken(TokenOptions tokenOptions, User user,
												   SigningCredentials signingCredentials,
												   IList<Role> roles, DateTime expireDate,
                                                   AddtionalParam? addtionalParam = default)
	{

        JwtSecurityToken jwt = new(
			tokenOptions.Issuer,
			tokenOptions.Audience,
            expires: expireDate,
            notBefore: Date.UtcNow,
            claims: SetClaims(user, roles, addtionalParam),
            signingCredentials: signingCredentials
		);
		return jwt;
	}


	private IEnumerable<Claim> SetClaims(User user, IList<Role> operationClaims, AddtionalParam? addtionalParam = default)
    {
        List<Claim> claims = new();
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        claims.AddUsername(user.Email);
        claims.AddFullName(user.FullName);
        if (addtionalParam!=null)  claims.AddParametr(addtionalParam);
        return claims;
    }


    public string  GetUsername(string? token=default)
    {
        if(token == null) token = GetToken();
        if (token.IsNullOrEmpty()) return string.Empty;
        return ReadToken(token).Claims?.Where(c => c.Type == Config.Username)?.FirstOrDefault()?.Value ?? string.Empty ;
    }  

   

    public (bool expire , string username) CheckExpireTime(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new ();
        JwtSecurityToken parsedToken = tokenHandler.ReadJwtToken(token);

        DateTime? expires = parsedToken.ValidTo;

        return ((bool)(expires > Date.UtcNow), GetUsername(token));

    }

    public  string GetToken()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

        if (authorizationHeader == StringValues.Empty) return string.Empty;
        var jwtHeader = authorizationHeader.ToList().FirstOrDefault(c => c != null && c.Contains("Bearer"));
        return jwtHeader != null ? jwtHeader.Split("Bearer").Last().Trim() : string.Empty;
    }

    public string IDToken()=>
          Guid.NewGuid().ToString();


    public IEnumerable<string>? GetRoles()
    {

        var token = GetToken();
        var roles = ReadToken(token).Claims.Select(c => c.Value);
        return roles.Any() ? roles : default;

    }


    private JwtSecurityToken ReadToken(string token)=>
         new JwtSecurityTokenHandler().ReadJwtToken(token);

    public string? GetOrganizationName()
    {
        var token = GetToken();
        return ReadToken(token).Claims.Where(c => c.Type == Config.OrganizationName)
            .Select(c => c.Value).FirstOrDefault();
    }

    public bool ExsitsRole(string roleName)=>
         GetRoles().Any(c => c == roleName);


    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,

            ValidIssuer = _tokenOptions.Issuer,
            ValidAudience = _tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                expires != null && expires > DateTime.UtcNow
        };
    }

    public bool  ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true ;
        }
        catch
        {
            return false;
        }
    }

    public string AddExpireTime(string token, int minute=5)
    {
        JwtSecurityToken securityToken =  ReadToken(token);

        // Create a new token with the same claims and an extended expiration time
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

        var newToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: securityToken.Claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(minute),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(newToken);
    }
}
