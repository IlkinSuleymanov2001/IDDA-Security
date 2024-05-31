using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.Business.Concretes
{
	public class UserManager : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private async Task EmailIsUniqueWhenUpdateEmail(int userId, string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email.ToLower() && u.Id != userId);
            if (user != null) throw new BusinessException("Email Addres Artiq isdifade olunur.");
        }


        public async Task Delete(DeleteUserRequest deleteUserRequest)
		{
			var deleteUser = await IfUserNotExistsThrow(deleteUserRequest.Id);
			await _userRepository.DeleteAsync(deleteUser);
		}

		

		public async Task<GetUserResponse> GetById(int userId)
		{
			var user = await IfUserNotExistsThrow(userId);
			return _mapper.Map<GetUserResponse>(user);
		}

		public async Task<PaginingGetListUserResponse> GetList(PageRequest pageRequest = null)
		{
			IPaginate<User> pageList;
			if (pageRequest == null)
				pageList = await _userRepository.GetListAsync();

			pageList = await _userRepository.GetListAsync(size: pageRequest.PageSize, index: pageRequest.Page);
			return _mapper.Map<PaginingGetListUserResponse>(pageList);
		}

		public async Task<UpdateUserResponse> UpdateUserEmail(UpdateUserEmailRequest updateUserRequest)
		{
			var user = await IfUserNotExistsThrow(updateUserRequest.Id);
			await EmailIsUniqueWhenUpdateEmail(updateUserRequest.Id , updateUserRequest.Email);
			user.Email = updateUserRequest.Email;
			await _userRepository.UpdateAsync(user);
			return _mapper.Map<UpdateUserResponse>(user);

		}

		public async Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest)
		{
			var user  =await IfUserNotExistsThrow(updateUserPasswordRequest.Id);
			var passwordIsTrust = HashingHelper.VerifyPasswordHash(updateUserPasswordRequest.CurrentPassword
				, user.PasswordHash, user.PasswordSalt);
			if (!passwordIsTrust)
				throw new BusinessException("Cari Password duzgun deyil..");

			byte[] newPasswordHash, newPasswordSalt;
			HashingHelper.CreatePasswordHash(updateUserPasswordRequest.Password,out  newPasswordHash
				,out  newPasswordSalt);

			user.PasswordHash = newPasswordHash;
			user.PasswordSalt = newPasswordSalt;
			await _userRepository.UpdateAsync(user);

		}

        public async Task UpadetUserNameAndSurname(UpdateNameAndSurnameRequest updateNameAndSurname)
        {
			var user = await IfUserNotExistsThrow(updateNameAndSurname.Id);
			user.FirstName = updateNameAndSurname.FirstName;
			user.LastName = updateNameAndSurname.LastName;
			await _userRepository.UpdateAsync(user);
        }

        private async Task<User> IfUserNotExistsThrow(int id)
        {
            var user = await _userRepository.GetAsync(u => u.Id == id);
            if (user == null) throw new BusinessException("Bele bir ID-li isdifadeci yoxdur..");
            return user;

        }
    }
}
