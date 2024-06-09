using Core.Security.Entities;
using Goverment.Core.Security.JWT;

namespace Core.Security.JWT;

public interface ITokenHelper
{
	object CreateToken(User user, IList<Role> roles);
    
	string GetUserEmail(string token);

    User GenerateAndSetOTP(User user);
    
}
