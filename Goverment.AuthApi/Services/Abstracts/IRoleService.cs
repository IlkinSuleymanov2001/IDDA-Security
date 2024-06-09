
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Services.Dtos.Request.Role;

namespace Goverment.AuthApi.Business.Abstracts
{
    public interface IRoleService
	{

		Task<CreateRoleResponse> Create(RoleRequest createRoleRequest);
		Task Delete(RoleRequest roleRequest);

		Task<GetByNameRoleResponse> GetByName(RoleRequest roleRequest);

		Task<IList<ListRoleResponse>> GetList();

		Task<UpdateRoleResponse> Update(UpdateRoleRequest updateRoleRequest);
        Task<PaginingGetListUserResponse> GetUserListByRole(UserListByRoleRequest @event);

        

    }
}
