using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Goverment.AuthApi.Repositories.Abstracts
{
    public interface IUserLoginSecurityRepository : IAsyncRepository<UserLoginSecurity>, IRepository<UserLoginSecurity>
    {

    }
}
