using Goverment.AuthApi.Business.Dtos.Request.UserRole;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Business.Dtos.Response.UserRole;

namespace Goverment.AuthApi.Business.Abstracts
{
	public interface  IUserRoleService
	{
		Task<CreateUserRoleResponse>  Add(AddUserRoleRequest addUserRoleRequest);
		Task Delete(DeleteUserRoleRequest deleteUserRoleRequest);
		Task AddRolesToUser(AddRolesToUserRequest addRolesToUserRequest);
		Task AddUsersToRole(AddUsersToRoleRequest addusersToRoleRequest);

		Task<PaginingGetListUserResponse> GetUserListByRoleId(GetUserListByRoleIdRequest getListUsersByRoleIdRequest);
		Task<IList<ListRoleResponse>> GetRoleListByUserId(int userId);

		Task DeleteRolesFromUser(int userId);
		Task DeleteUsersFromRole(int roleId);

	}
}
