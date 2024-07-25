using Core.Application.Requests;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Role;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Services.Dtos.Request.User;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
namespace Goverment.AuthApi.Business.Abstracts;

public interface IUserService
	{

	Task<IDataResponse<CreateUserResponse>> Create(CreateUserRequest createUserRequest,string? organizationName, params string?[]? role);

    Task<IResponse> DeleteYourSelf(DeleteUserRequest deleteUser);
    Task<IResponse> Delete(string username);

    Task<IDataResponse<GetUserResponse>> GetByEmail(string email);

    Task<IDataResponse<GetUserResponse>> Get();
    Task<IDataResponse<GetPermissionsUserResponse>> GetForWeb();

    Task<IDataResponse<PaginingGetListUserResponse>> GetList(PageRequest pageRequest);

	Task<IResponse> UpdatePassword(UpdateUserPasswordRequest updateUserPasswordRequest);

	Task<IResponse> UpdateFullName(UpdateUserFullNameRequest updateNameAndSurname);

    Task<IResponse> AddRole(UserRoleRequest userRole);

    Task<IResponse> AddRoleRange(AddRolesToUserRequest userRoles);

    Task<IResponse> DeleteRole(UserRoleRequest userRole);

    Task<IResponse> DeleteRoleRange(UserEmailRequest userEmail);
    Task<IDataResponse<IList<ListRoleResponse>>> GetRoleList(UserEmailRequest UserEmail );


}
