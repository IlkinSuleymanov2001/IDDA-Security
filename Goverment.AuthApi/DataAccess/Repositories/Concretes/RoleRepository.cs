using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.DataAccess.Context;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.DataAccess.Repositories.Concretes
{
	public class RoleRepository : EfRepositoryBase<Role, AuthContext>, IRoleRepository
	{
		public RoleRepository(AuthContext context) : base(context)
		{
            Console.WriteLine("salam repository ");
        }
	}
}
