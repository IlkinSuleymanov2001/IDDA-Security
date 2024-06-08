using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Goverment.AuthApi.Repositories.Abstracts
{
    public interface IRoleRepository : IRepository<Role>, IAsyncRepository<Role>
    {
    }
}
