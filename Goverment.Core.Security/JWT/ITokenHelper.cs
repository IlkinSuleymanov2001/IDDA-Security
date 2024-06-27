using Core.Security.Entities;
using Goverment.Core.Security.JWT;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    Tokens CreateTokens(User user, IList<Role> roles);
    
	string GetUsername(string token=null);

    (bool expire, string username) ParseJwtAndCheckExpireTime(string token);
    string IDToken();


}
