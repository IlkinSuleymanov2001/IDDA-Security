using Core.Security.Entities;
using Goverment.Core.Security.JWT;

namespace Core.Security.JWT;

public interface ITokenHelper
{
	
	Token CreateToken(User user, IList<Role> roles);

	string  CreateConfirmToken(User user);

	int GetUserIdFromToken(string token);

    void ConfirmTokenParse(string confirmToken, out string email, out int roleId);

}
