using Core.Persistence.Repositories;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.Core.Security.Entities;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class UserResendOtpSecurityRepository : EfRepositoryBase<UserResendOtpSecurity, AuthContext>, IUserOtpSecurityRepository
    {
        public UserResendOtpSecurityRepository(AuthContext context) : base(context)
        {
        }
    }
}
