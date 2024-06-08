using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Goverment.AuthApi.Repositories.Abstracts
{
    public interface IUserRoleRepository : IRepository<UserRole>, IAsyncRepository<UserRole>
    {

    }
}
