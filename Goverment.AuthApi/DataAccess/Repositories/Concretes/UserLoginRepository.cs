using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.DataAccess.Context;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.DataAccess.Repositories.Concretes
{
    public class UserLoginRepository : EfRepositoryBase<UserLoginSecurity, AuthContext>, IUserLoginSecurityRepository
    {
        public UserLoginRepository(AuthContext context) : base(context)
        {
        }
    }
}
