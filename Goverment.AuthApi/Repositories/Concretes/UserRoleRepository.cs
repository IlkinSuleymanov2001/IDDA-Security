using Core.Persistence.Repositories;
using Core.Security.Entities;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Repositories.Concretes.Contexts;

namespace Goverment.AuthApi.Repositories.Concretes
{
    public class UserRoleRepository(AuthContext context)
        : EfRepositoryBase<UserRole, AuthContext>(context), IUserRoleRepository;
}
