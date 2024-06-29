
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;

namespace Goverment.AuthApi.Business.Abstracts
{
    public interface IRoleService
	{

		Task<IDataResponse<CreateRoleResponse>> Create(RoleRequest createRoleRequest);
		Task<IResponse> Delete(RoleRequest roleRequest);

		Task<IDataResponse<GetByNameRoleResponse>> GetByName(RoleRequest roleRequest);

		Task<IDataResponse<IList<ListRoleResponse>>> GetList();

		Task<IDataResponse<UpdateRoleResponse>> Update(UpdateRoleRequest updateRoleRequest);
        Task<IDataResponse<PaginingGetListUserResponse>> GetUserListByRole(UserListByRoleRequest @event);

        

    }
}
