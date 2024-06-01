using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Goverment.AuthApi.DataAccess.Repositories.Abstracts
{
    public interface IUserLoginSecurityRepository:IAsyncRepository<UserLoginSecurity>,IRepository<UserLoginSecurity>
    {

    }
}
