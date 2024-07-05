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

	Task<IDataResponse<CreateUserResponse>> Create(CreateUserRequest createUserRequest, params string?[] role);

    Task<IResponse> Delete(DeleteUserRequest deleteUser);

	Task<IDataResponse<GetUserResponse>> GetByEmail(string email);

    Task<IDataResponse<GetUserResponse>> Get();

    Task<IDataResponse<PaginingGetListUserResponse>> GetList(PageRequest pageRequest=null);

	Task<IResponse> UpdatePassword(UpdateUserPasswordRequest updateUserPasswordRequest);

	Task<IResponse> UpdateFullName(UpdateUserFullNameRequest updateNameAndSurname);

    Task<IResponse> AddRole(UserRoleRequest userrole);

    Task<IResponse> AddRoleRange(AddRolesToUserRequest userroles);

    Task<IResponse> DeleteRole(UserRoleRequest userrole);

    Task<IResponse> DeleteRoleRange(UserEmailRequest userEmail);
    Task<IDataResponse<IList<ListRoleResponse>>> GetRoleList(UserEmailRequest UserEmail );


}
