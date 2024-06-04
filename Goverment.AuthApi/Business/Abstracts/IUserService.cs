using Core.Application.Requests;
using Core.Security.Entities;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;

namespace Goverment.AuthApi.Business.Abstracts;

    public interface IUserService
	{

	Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest);

    Task Delete();

	Task<GetUserResponse> GetById(int  userId);

    Task<GetUserResponse> Get();

    Task<PaginingGetListUserResponse> GetList(PageRequest pageRequest=null);

	Task UpdateUserEmail(UpdateUserEmailRequest updateUserRequest);

	Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest);

	Task UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname);
 
    }
