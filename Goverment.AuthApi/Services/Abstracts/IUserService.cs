using Core.Application.Requests;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;

namespace Goverment.AuthApi.Business.Abstracts;

    public interface IUserService
	{

	Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest);

    Task Delete();

	Task<GetUserResponse> GetByEmail(string email);

    Task<GetUserResponse> Get();

    Task<PaginingGetListUserResponse> GetList(PageRequest pageRequest=null);

	Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest);

	Task UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname);
 
    }
