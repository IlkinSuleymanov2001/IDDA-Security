using Core.Persistence.Repositories;
using Goverment.Core.Security.Entities;

namespace Goverment.AuthApi.Repositories.Abstracts
{
    public interface  IUserOtpSecurityRepository :IAsyncRepository<UserOtpSecurity>
    {
    }
}
