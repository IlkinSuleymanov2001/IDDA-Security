using Core.Security.Entities;
using Goverment.Core.Security.JWT;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    Tokens CreateTokens(User user, IList<Role> roles, AddtionalParam? param = default);
    
	string GetUsername(string? token=default);
    IEnumerable<string>? GetRoles();
    string? GetOrganizationName();
    (bool expire, string username) CheckExpireTime(string token);
    string IDToken();
    bool ValidateToken(string token);
    string AddExpireTime(string token, int minute = 5);
    string GetToken();
    bool ExsitsRole(string roleName);



}
