using Core.Application.Requests;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.Role;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Services.Dtos.Request.Role;
using Goverment.AuthApi.Services.Dtos.Request.User;

namespace Goverment.AuthApi.Business.Abstracts;

public interface IUserService
	{

	Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest);

    Task Delete(DeleteUserRequest deleteUser);

	Task<GetUserResponse> GetByEmail(string email);

    Task<GetUserResponse> Get();

    Task<PaginingGetListUserResponse> GetList(PageRequest pageRequest=null);

	Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest);

	Task UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname);

    Task AddRole(UserRoleRequest @event);

    Task AddRoleRange(AddRolesToUserRequest @event);

    Task DeleteRole(UserRoleRequest @event);

    Task DeleteRoleRange(UserEmailRequest @event);
    Task<IList<ListRoleResponse>> GetRoleList(UserEmailRequest @event );


}
