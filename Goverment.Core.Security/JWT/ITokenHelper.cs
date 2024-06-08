using Core.Security.Entities;

namespace Core.Security.JWT;

public interface ITokenHelper
{
	object CreateToken(User user, IList<Role> roles);

	string GetUserEmail(string token);

    User GenerateAndSetOTP(User user);
    
}
