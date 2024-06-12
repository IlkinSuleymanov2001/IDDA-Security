using Core.Security.Entities;
using Goverment.Core.Security.JWT;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    Tokens CreateTokens(User user, IList<Role> roles);
    
	string GetUserEmail(string token);

    User GenerateAndSetOTP(User user);

    (bool expire, string username) ParseJwtAndCheckExpireTime(string token);
    
}
