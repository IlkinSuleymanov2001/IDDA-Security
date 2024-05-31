using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.DataAccess.Context;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.DataAccess.Repositories.Concretes
{
	public class UserRepository : EfRepositoryBase<User, AuthContext>, IUserRepository
	{
		public UserRepository(AuthContext context) : base(context)
		{

		}
	}
}
