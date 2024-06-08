using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.Repositories.Abstracts;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class RoleRepository : EfRepositoryBase<Role, AuthContext>, IRoleRepository
    {
        public RoleRepository(AuthContext context) : base(context)
        {
            Console.WriteLine("salam repository ");
        }
    }
}
