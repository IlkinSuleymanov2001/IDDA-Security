
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Response.Role;

namespace Goverment.AuthApi.Business.Abstracts
{
    public interface IRoleService
	{

		Task<CreateRoleResponse> Create(CreateRoleRequest createRoleRequest);
		Task Delete(DeleteRoleRequest deleteRoleRequest);

		Task<GetByIdRoleResponse> GetById(int  roleId);

		Task<IList<ListRoleResponse>> GetList();

		Task<UpdateRoleResponse> Update(UpdateRoleRequest updateRoleRequest);

	}
}
