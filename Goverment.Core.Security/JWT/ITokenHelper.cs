using Core.Security.Entities;
using Goverment.Core.Security.JWT;

namespace Core.Security.JWT;

public interface ITokenHelper
{
	
	Token CreateToken(User user, IList<Role> roles);

	RefreshToken CreateRefreshToken(User user);

	string  CreateConfirmToken(User user);
	void ConfirmTokenParse(string confirmToken, out string email, out int roleId);

}
