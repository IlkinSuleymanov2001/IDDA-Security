using Core.Persistence.Repositories;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Repositories.Concretes.Contexts;
using Goverment.Core.Security.Entities;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class UserResendOtpSecurityRepository(AuthContext context)
        : EfRepositoryBase<UserResendOtpSecurity, AuthContext>(context), IUserOtpSecurityRepository;
}
