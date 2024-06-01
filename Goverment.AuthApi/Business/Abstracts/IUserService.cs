using Core.Application.Requests;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;

namespace Goverment.AuthApi.Business.Abstracts;

    public interface IUserService
	{
	//Task<CreateUserResponse> Create(CreateUserRequest createUserRequest);

	Task Delete(DeleteUserRequest deleteUserRequest);

	Task<GetUserResponse> GetById(int  userId);

	Task<PaginingGetListUserResponse> GetList(PageRequest pageRequest=null);

	Task<string> UpdateUserEmail(UpdateUserEmailRequest updateUserRequest);

	Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest);

	Task UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname);

	Task VerifyNewEmail(VerifyingRequest verifyingRequest);

	/*Task DeletePhoto(int userId);
        Task UpdatePhoto(int userId,string beforePhotoUrl,  IFormFile file);
	Task UploadPhoto(int userId, IFormFile file);*/

    }
