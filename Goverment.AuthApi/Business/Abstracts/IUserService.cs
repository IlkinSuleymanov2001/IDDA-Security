using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.Core.Persistance.Repositories;

namespace Goverment.AuthApi.Business.Abstracts
{
    public interface IUserService
	{
		//Task<CreateUserResponse> Create(CreateUserRequest createUserRequest);

		Task Delete(DeleteUserRequest deleteUserRequest);

		Task<GetUserResponse> GetById(int  userId);

		Task<PaginingGetListUserResponse> GetList(PageRequest pageRequest=null);

		Task<UpdateUserResponse> UpdateUserEmail(UpdateUserEmailRequest updateUserRequest);

		Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest);

		Task UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname);
	}
}
