using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Repositories.Concretes.Contexts;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class UserRepository : EfRepositoryBase<User, AuthContext>, IUserRepository
    {
        public UserRepository(AuthContext context) : base(context)
        {

        }
    }
}
