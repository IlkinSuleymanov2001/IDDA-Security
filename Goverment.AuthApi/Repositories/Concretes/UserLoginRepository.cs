using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.Repositories.Abstracts;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class UserLoginRepository : EfRepositoryBase<UserLoginSecurity, AuthContext>, IUserLoginSecurityRepository
    {
        public UserLoginRepository(AuthContext context) : base(context)
        {
        }
    }
}
