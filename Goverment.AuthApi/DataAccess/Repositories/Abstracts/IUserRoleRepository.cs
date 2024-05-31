using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Goverment.AuthApi.DataAccess.Repositories.Abstracts
{
	public interface IUserRoleRepository:IRepository<UserRole>, IAsyncRepository<UserRole>
	{

	}
}
