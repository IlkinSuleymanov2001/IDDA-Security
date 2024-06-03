using AutoMapper;
using Core.Application.Requests;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing.MailKitImplementations;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.AuthApi.Business.Utlilities;
using Goverment.AuthApi.Business.Utlilities.Caches;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;

namespace Goverment.AuthApi.Business.Concretes
{
	public class UserManager : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ICacheService _cacheService;
        private readonly ITokenHelper _jwtService;
		private readonly int _currentUserId;


        public UserManager(IUserRepository userRepository, IMapper mapper, 
			ICacheService cacheService, ITokenHelper token, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _jwtService = token;
            _currentUserId = _jwtService.GetUserIdFromToken(Helper.GetToken(httpContextAccessor));
        }


        public async Task Delete()
		{
			var deleteUser = await IfUserNotExistsThrow(_currentUserId);
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

		public async Task<string> UpdateUserEmail(UpdateUserEmailRequest updateUserRequest)
		{
			var user = await IfUserNotExistsThrow(_currentUserId);
            await EmailIsUniqueWhenUpdateEmail(_currentUserId , updateUserRequest.Email);
            user.Email = updateUserRequest.Email;
            Gmail.OtpSend(user);
			var userCacheId = Helper.getCacheJsonId();
			_cacheService.SetData(userCacheId, user,DateTimeOffset.Now.AddMinutes(5));
			//await _userRepository.UpdateAsync(user);
			return userCacheId;

		}
        public async Task VerifyNewEmail(VerifyingRequest verifyingRequest)
        {
			var user = _cacheService.GetData<User>(verifyingRequest.CacheUserId);
            IfNullUserThrows(user);
			Helper.CheckOtpAndTime(user, verifyingRequest.OtpCode);
			await _userRepository.UpdateAsync(user);
			_cacheService.RemoveData(verifyingRequest.CacheUserId);
        }

        public async Task UpdateUserPassword(UpdateUserPasswordRequest updateUserPasswordRequest)
		{
           

            var user  =await IfUserNotExistsThrow(_currentUserId);
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
		

            var user = await IfUserNotExistsThrow(_currentUserId);
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
        private async Task EmailIsUniqueWhenUpdateEmail(int userId, string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email.ToLower() && u.Id != userId);
            if (user != null) throw new BusinessException("Email Addres Artiq isdifade olunur.");
        }

        private void IfNullUserThrows(User user)
        {
            if (user == default) throw new BusinessException("user not found");
        }


    }
}
