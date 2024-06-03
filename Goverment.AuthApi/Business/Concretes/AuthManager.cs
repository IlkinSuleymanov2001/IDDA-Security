using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing.MailKitImplementations;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Request.UserRole;
using Goverment.AuthApi.Business.Utlilities;
using Goverment.AuthApi.Business.Utlilities.Caches;
using Goverment.AuthApi.DataAccess.Repositories.Abstracts;
using Goverment.Core.Security.JWT;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Business.Concretes
{
    public class AuthManager : IAuthService
	{
		private readonly ITokenHelper _jwtService;
		private readonly IUserRoleService _userRoleService;
		private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly DateTimeOffset _cacheLifeCycle;
        private readonly IUserLoginSecurityRepository   _loginSecurityRepository;
        private readonly int _currentUserId;


        public AuthManager(ITokenHelper jwtService,
            IUserRoleService userRoleService,
            IUserRepository userRepository, IMapper mapper
          , ICacheService cacheService,
            IUserLoginSecurityRepository loginSecurityRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _jwtService = jwtService;
            _userRoleService = userRoleService;
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
            _cacheLifeCycle = DateTimeOffset.Now.AddMinutes(20);
            _loginSecurityRepository = loginSecurityRepository;
            _currentUserId = _jwtService.GetUserIdFromToken(Helper.GetToken(httpContextAccessor));
        }

        public async Task<Token> Login(UserLoginRequest userLoginRequest)
		{
           
            var user = await FindUserByEmail(userLoginRequest.Email);
            if (!user.IsVerify) throw new AuthorizationException("zehmet olmasa email -inizi tediqleyin.");

            CheckUserBlock(user);

            var isTruePassword = HashingHelper.VerifyPasswordHash(userLoginRequest.Password,user.PasswordHash, user.PasswordSalt);

            if (!isTruePassword)
            {
                SendWarningMessage(user);
                await LoginLimitExceed(user);
                throw new AuthorizationException("Password duzgun deyil..");
            }

            user.Status = true;
            user.UserLoginSecurity.LoginRetryCount = 0;
            await _userRepository.UpdateAsync(user);
            var rolesResponse = await _userRoleService.GetRoleListByUserId(user.Id);
            var roleList = _mapper.Map<IList<Role>>(rolesResponse);
          
            return _jwtService.CreateToken(user, roleList);
		}

        

        public async Task<string> Register(CreateUserRequest createUserRequest)
		{
            var unicId = Helper.getCacheJsonId();
            var user = await CreateUser(createUserRequest);
            Gmail.OtpSend(user);
            _cacheService.SetData<User>(unicId,user, _cacheLifeCycle);
            //await _userRepository.UpdateAsync(user);  
             return unicId;

		}

        public async  Task  VerifyAccount(VerifyingRequest accountRequest)
		{
            //var user = await FindUserById(verifyOtpCodeRequest.UserId);
            var user = _cacheService.GetData<User>(accountRequest.CacheUserId);
            IfNullUserThrows(user);
            Helper.CheckOtpAndTime(user, accountRequest.OtpCode);

            user.IsVerify = true;
            await _userRepository.AddAsync(user);

            await _userRoleService.Add(new AddUserRoleRequest
            {
                RoleId = JwtHelper.defaultRoleIdWhenUserCreated,
                UserId = user.Id
            });

             await _loginSecurityRepository.AddAsync(new UserLoginSecurity { UserId = user.Id,LoginRetryCount=0});
            _cacheService.RemoveData(accountRequest.CacheUserId);

		}

        public async Task VerifyOTPForResetPassword(VerifyingRequest verifyForgetPassword)
        {
            //var user = await FindUserById(verifyForgetPassword.UserId);
            var user = _cacheService.GetData<User>(verifyForgetPassword.CacheUserId);
            IfNullUserThrows(user);
            Helper.CheckOtpAndTime(user, verifyForgetPassword.OtpCode);
            user.IsResetPassword = true;
            await _userRepository.UpdateAsync(user);
            _cacheService.RemoveData(verifyForgetPassword.CacheUserId);

        }

       

        public async Task<string> ForgetPassword(string email)
        {
            User user = await FindUserByEmail(email);
            if (!user.IsVerify) throw new BusinessException("mailinizi tesdiq etmeden passwordu yeniden teyin ede bilmezsen  ");
            Gmail.OtpSend(user);
            var userId = Helper.getCacheJsonId();
            _cacheService.SetData(userId,user,_cacheLifeCycle);
            return userId;
            //_userRepository.Update(user);

        }

        
        public async Task ReGenerateOTP(string userId)
        {
            // var user = await FindUserById(userId);
            var user = _cacheService.GetData<User>(userId);
            IfNullUserThrows(user);
            Gmail.OtpSend(user);
            _cacheService.SetData(userId, user, DateTimeOffset.Now.AddMinutes(3));
           // _userRepository.Update(user);
        }


        public async Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest)
        {
            User user = await FindUserById(resetUserPasswordRequest.UserId);
            if (!user.IsResetPassword) throw new BusinessException("zehmet olmasa otp kodunuzu tesdiqleyin");
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(resetUserPasswordRequest.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsResetPassword = false;
            await _userRepository.UpdateAsync(user);

        }
      

        private void SendWarningMessage(User user)
        {
            if (user.UserLoginSecurity.LoginRetryCount is 2 ||
                user.UserLoginSecurity.LoginRetryCount is 7||
                user.UserLoginSecurity.LoginRetryCount is 12)
                Gmail.SendWarningMessage(user);
        }

        private async Task LoginLimitExceed(User user)
        {
            if (user.UserLoginSecurity.LoginRetryCount is 14)
            {
                user.UserLoginSecurity.IsAccountBlock = true;
                user.UserLoginSecurity.AccountBlockedTime = DateTime.Now;
                user.UserLoginSecurity.AccountUnblockedTime = DateTime.Now.AddDays(1);

            }
            else if (user.UserLoginSecurity.LoginRetryCount is 9)
            {
                user.UserLoginSecurity.IsAccountBlock = true;
                user.UserLoginSecurity.AccountBlockedTime = DateTime.Now;
                user.UserLoginSecurity.AccountUnblockedTime = DateTime.Now.AddHours(1);

            }
            else if (user.UserLoginSecurity.LoginRetryCount is 4)
            {
                user.UserLoginSecurity.IsAccountBlock = true;
                user.UserLoginSecurity.AccountBlockedTime = DateTime.Now;
                user.UserLoginSecurity.AccountUnblockedTime = DateTime.Now.AddMinutes(15);

            }

            user.UserLoginSecurity.LoginRetryCount += 1;
            await _userRepository.UpdateAsync(user);
        }

        private User CheckUserBlock(User user)
        {
            if (user.UserLoginSecurity.IsAccountBlock)
            {
                DateTime endBlockTime = user.UserLoginSecurity.AccountUnblockedTime ?? DateTime.UtcNow;
                int minute = (int)(endBlockTime - DateTime.Now).TotalMinutes;
                if (minute > 0)
                {
                    throw new AuthorizationException($"cox cehd etdiniz,{minute} deqqiqe  sonra yeniden cehd edin ");
                }
                else
                {
                    user.UserLoginSecurity.IsAccountBlock = false;
                    ClearIfRetryCountMax(user);
                }

            }
            return user;
        }

        private void ClearIfRetryCountMax(User user)
        {
            if (user.UserLoginSecurity.LoginRetryCount is 15)
                 user.UserLoginSecurity.LoginRetryCount = 0;
        }

        private  void IfNullUserThrows(User user) 
        {
            if (user == default) throw new BusinessException("user not found");
        }
           


        private async Task EmailIsUniqueWhenUserCreated(string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null) throw new BusinessException($"{email} Addres Artiq isdifade olunur.");
        }
      
        private async Task<User> FindUserByEmail(string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email.ToLower(),include:ef=>ef.Include(e=>e.UserLoginSecurity));
            if (user == null) throw new BusinessException("hesab movcud deyil");
            return user;
        }

        private async Task<User> FindUserById(int id)
        {
            var user = await _userRepository.GetAsync(u => u.Id == id);
            if (user == null) throw new BusinessException($"{id} uygun user tapilmadi");
            return user;
        }

        private async Task<User> CreateUser(CreateUserRequest createUserRequest)
        {
            await EmailIsUniqueWhenUserCreated(createUserRequest.Email);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(createUserRequest.Password, out passwordHash, out passwordSalt);

            User user = new User
            {
                Email = createUserRequest.Email.ToLower(),
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsVerify = false,
                IsResetPassword = false
            };
           // await _userRepository.AddAsync(user);
            return user;
        }

        public Task RegisterWithConfirmToken(CreateUserRequest createUserRequest)
        {
            throw new NotImplementedException();
        }

        public Task VerifyAccount(string verifConfirm)
        {
            throw new NotImplementedException();
        }

        public async Task Logout()
        {
            
           var user = await FindUserById(_currentUserId);
           user.Status = false;
           await _userRepository.UpdateAsync(user);

        }


        /* private async Task CheckConfrimToken(User user, string confirmToken)
         {
             if (user.ConfirmToken != confirmToken) throw new BusinessException("biz security app yazirig yazanda.");
             user.ConfirmToken = null;
             user.IsVerify = true;
             await _userRepository.UpdateAsync(user);
         }*/


    }
}
