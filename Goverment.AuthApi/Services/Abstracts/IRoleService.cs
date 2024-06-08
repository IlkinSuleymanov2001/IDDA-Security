
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Response.Role;

namespace Goverment.AuthApi.Business.Abstracts
{
    public interface IRoleService
	{

		Task<CreateRoleResponse> Create(CreateRoleRequest createRoleRequest);
		Task Delete(DeleteRoleRequest deleteRoleRequest);

		Task<GetByNameRoleResponse> GetByName(string name);

		Task<IList<ListRoleResponse>> GetList();

		Task<UpdateRoleResponse> Update(UpdateRoleRequest updateRoleRequest);

	}
}
