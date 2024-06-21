using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing.MailKitImplementations;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Utlilities;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Auth;
using Goverment.Core.CrossCuttingConcers.Results;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.JWT;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Business.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHelper _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUserLoginSecurityRepository _loginSecurityRepository;
        private readonly IUserOtpSecurityRepository _otpSecurityRepository;
        private readonly string  _currentUser;


        public AuthService(ITokenHelper jwtService,
            IUserRepository userRepository, IMapper mapper,
            IUserLoginSecurityRepository loginSecurityRepository,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository, 
            IUserOtpSecurityRepository otpSecurityRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _mapper = mapper;
            _loginSecurityRepository = loginSecurityRepository;
            _currentUser = _jwtService.GetUsername();
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _otpSecurityRepository = otpSecurityRepository;
        }

        public async Task<Tokens> Login(UserLoginRequest userLoginRequest)
        {

            var user = await FindUserByEmail(userLoginRequest.Email);
            if (!user.IsVerify) 
            {
                await ReGenerateOTP(user);
                throw new AuthorizationException("zehmet olmasa email -inizi tediqleyin.");
            }

            CheckUserBlock(user);

            var isTruePassword = HashingHelper.VerifyPasswordHash(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt);

            if (!isTruePassword)
            {
                SendWarningMessage(user);
                await LoginLimitExceed(user);
                throw new AuthorizationException("Password duzgun deyil..");
            }

            user.Status = true;
            user.UserLoginSecurity.LoginRetryCount = 0;

            UnBlockOtp(user);

            await _userRepository.UpdateAsync(user);

          return _jwtService.CreateTokens(user,await GetRoles(user));

        }

      


        public async Task<AccesTokenResponse> LoginWithRefreshToken(RefreshTokenRequest tokenRequest)
        {
            var response = _jwtService.ParseJwtAndCheckExpireTime(tokenRequest.Token);
            if (!response.expire)
                throw new AuthorizationException("invalid token");

             var user = await _userRepository.GetAsync(predicate:u => u.Email == response.username,
             include: ef => ef.Include(e => e.UserRoles).ThenInclude(u=>u.Role));

            return new AccesTokenResponse
            {
                Token = _jwtService.CreateTokens(user, await GetRoles(user)).AccessToken
            };

        }


        public async Task Register(CreateUserRequest createUserRequest)
        {
            
            var user = await CreateUser(createUserRequest);
            _jwtService.GenerateAndSetOTP(user);
            await _userRepository.AddAsync(user);
            await _otpSecurityRepository.AddAsync(new UserOtpSecurity { UserId = user.Id });
            Gmail.OtpSend(user);

        }

        public async Task VerifyAccount(VerifyingRequest accountRequest)
        {
            User user = await FindUserByOtp(accountRequest.OtpCode);

            Helper.CheckOtpAndTime(user, accountRequest.OtpCode);
            user.IsVerify = true;
            user.OtpCode = null;
            user.UserRoles.Add(new UserRole { RoleId = _Role.Id, UserId = user.Id });
            user.UserLoginSecurity = new UserLoginSecurity { UserId = user.Id, LoginRetryCount = 0 };
            await _userRepository.UpdateAsync(user);

        }


        public async Task ReGenerateOTP(UserEmailRequest emailRequest)
        {
            
            var user = await FindUserByEmail(emailRequest.Email);

            CheckOtpBlock(user);

            _jwtService.GenerateAndSetOTP(user);
            await  _userRepository.UpdateAsync(user);
            Gmail.OtpSend(user);
        }

        public async Task ReGenerateOTP(User user)
        {

            CheckOtpBlock(user);

            _jwtService.GenerateAndSetOTP(user);
            await _userRepository.UpdateAsync(user);
            Gmail.OtpSend(user);
        }

        public async Task<DataResult> OtpIsTrust(VerifyingRequest verifyingRequest)
        {
            User user = await FindUserByOtp(verifyingRequest.OtpCode);
            Helper.CheckOtpAndTime(user, verifyingRequest.OtpCode);
            user.OtpCode = null;
            user.OptCreatedDate = null;
            user.IDToken = _jwtService.IDToken();
            user.IDTokenExpireDate = DateTime.UtcNow.AddMinutes(3);
            await _userRepository.UpdateAsync(user);
            return new SuccessDataResult(data: user.IDToken, "otp is confirm..");
        }

        public async Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest, string idToken)
        {

            User? user = await _userRepository.GetAsync(c => c.IDToken == idToken);
            if (user == null) throw new BusinessException("invalid IDToken");


            Helper.CheckIDTokenExpireTime(user);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(resetUserPasswordRequest.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IDToken = null;
            user.IDTokenExpireDate = null;
            await _userRepository.UpdateAsync(user);


        }

        private void   CheckOtpBlock(User user) 
        {
            if (user.UserOtpSecurity.IsLock)
            {
                if (user.UserOtpSecurity.unBlockDate > DateTime.UtcNow)
                    throw new BusinessException("bir az sonra cehd edin... ");
                else
                    UnBlockOtp(user);
            }
            else if (user.UserOtpSecurity.TryOtpCount is Helper.OtpMaxCount)
            {
                user.UserOtpSecurity.IsLock = true;
                user.UserOtpSecurity.unBlockDate = DateTime.UtcNow.AddHours(2);
            }
            
            user.UserOtpSecurity.TryOtpCount++;

        }
      

        

        private async Task<IList<Role>> GetRoles(User user)
        {
            var roles = new List<Role>();
            if (user.UserRoles.Count is 0) 
            {
               var datas= await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id, include: ef => ef.Include(ur => ur.Role));
                user.UserRoles = datas.Items;
            }

            foreach (var data in user.UserRoles)
                roles.Add(data.Role);
            return roles;
        }
        private void UnBlockOtp(User user)
        {
            user.UserOtpSecurity.IsLock = false;
            user.UserOtpSecurity.TryOtpCount = 0;
        }



        private void SendWarningMessage(User user)
        {
            if (user.UserLoginSecurity.LoginRetryCount is 2 )
                 Gmail.SendWarningMessage(user);
        }

        private async Task LoginLimitExceed(User user)
        {
            if(user.UserLoginSecurity.LoginRetryCount is 4)
            {
                user.UserLoginSecurity.IsAccountBlock = true;
                user.UserLoginSecurity.AccountBlockedTime = DateTime.UtcNow;
                user.UserLoginSecurity.AccountUnblockedTime = DateTime.UtcNow.AddMinutes(15);

            }
            else if (user.UserLoginSecurity.LoginRetryCount is 9)
            {
                user.UserLoginSecurity.IsAccountBlock = true;
                user.UserLoginSecurity.AccountBlockedTime = DateTime.UtcNow;
                user.UserLoginSecurity.AccountUnblockedTime = DateTime.UtcNow.AddHours(1);

            }
            else if (user.UserLoginSecurity.LoginRetryCount is 14)
            {
                user.UserLoginSecurity.IsAccountBlock = true;
                user.UserLoginSecurity.AccountBlockedTime = DateTime.UtcNow;
                user.UserLoginSecurity.AccountUnblockedTime = DateTime.UtcNow.AddDays(1); ;

            }

            user.UserLoginSecurity.LoginRetryCount += 1;
            await _userRepository.UpdateAsync(user);
        }

        private User CheckUserBlock(User user)
        {
            if (user.UserLoginSecurity.IsAccountBlock)
            {
                DateTime endBlockTime = user.UserLoginSecurity.AccountUnblockedTime ?? DateTime.UtcNow;
                int minute = (int)(endBlockTime - DateTime.UtcNow).TotalMinutes;
                if (minute > 0)
                {
                    throw new AuthorizationException("cox cehd etdiniz");
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

        private async Task EmailIsUniqueWhenUserCreated(string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null) throw new BusinessException($"{email} Addres Artiq isdifade olunur.");
        }

        private async Task<User> FindUserByEmail(string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email.ToLower(),
                include: ef => ef.Include(e => e.UserLoginSecurity).Include(e=>e.UserOtpSecurity));
            if (user == null) throw new BusinessException("hesab movcud deyil");
            return user;
        }

        private async Task<User> FindUserByOtp(string otp)
        {
            var user = await _userRepository.GetAsync(u => u.OtpCode == otp,
                include:ef=>ef.Include(c=>c.UserLoginSecurity));
            if (user == null) throw new BusinessException("otp-e duzgun deyil");
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
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsVerify = false
            };

            return user;
        }

     
    }
}

