using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.DataAccess.Context;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.DataAccess.Repositories.Concretes
{
	public class UserRoleRepository : EfRepositoryBase<UserRole, AuthContext>, IUserRoleRepository
	{
		public UserRoleRepository(AuthContext context) : base(context)
		{
		}
	}
}
