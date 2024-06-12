using Core.Persistence.Repositories;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.Core.Security.Entities;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class UserOtpSecurityRepository : EfRepositoryBase<UserOtpSecurity, AuthContext>, IUserOtpSecurityRepository
    {
        public UserOtpSecurityRepository(AuthContext context) : base(context)
        {
        }
    }
}
