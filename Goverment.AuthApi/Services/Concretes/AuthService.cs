using AutoMapper;
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
using Goverment.AuthApi.Business.Utlilities;
using Goverment.AuthApi.Repositories.Abstracts;
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
        private readonly string  _currentUser;


        public AuthService(ITokenHelper jwtService,
            IUserRepository userRepository, IMapper mapper,
            IUserLoginSecurityRepository loginSecurityRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _mapper = mapper;
            _loginSecurityRepository = loginSecurityRepository;
            _currentUser = _jwtService.GetUserEmail(Helper.GetToken(httpContextAccessor));
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<object> Login(UserLoginRequest userLoginRequest)
        {

            var user = await FindUserByEmail(userLoginRequest.Email);
          if (!user.IsVerify) throw new AuthorizationException("zehmet olmasa email -inizi tediqleyin.");
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
            await _userRepository.UpdateAsync(user);

          return _jwtService.CreateToken(user,await GetRoles(user));

        }


        public async Task Register(CreateUserRequest createUserRequest)
        {
            
            var user = await CreateUser(createUserRequest);
            _jwtService.GenerateAndSetOTP(user);
            await _userRepository.AddAsync(user);
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
           _jwtService.GenerateAndSetOTP(user);
           await  _userRepository.UpdateAsync(user);
            Gmail.OtpSend(user);
        }


        public async Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest)
        {

            User user = await FindUserByOtp(resetUserPasswordRequest.otpCode);

            Helper.CheckOtpAndTime(user, resetUserPasswordRequest.otpCode);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(resetUserPasswordRequest.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.OtpCode = null;
            user.UserLoginSecurity.AccountUnblockedTime = DateTime.UtcNow.AddMinutes(-1);
            await _userRepository.UpdateAsync(user);


        }

        private async Task<IList<Role>> GetRoles(User user)
        {
            var roles = new List<Role>();
            IPaginate<UserRole> datas = await _userRoleRepository.GetListAsync(ur => ur.UserId == user.Id, include: ef => ef.Include(ur => ur.Role));
            foreach (var data in datas.Items)
                roles.Add(data.Role);

            return roles;
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
                include: ef => ef.Include(e => e.UserLoginSecurity));
            if (user == null) throw new BusinessException("hesab movcud deyil");
            return user;
        }

        private async Task<User> FindUserByOtp(string otp)
        {
            var user = await _userRepository.GetAsync(u => u.OtpCode == otp,
                include:ef=>ef.Include(c=>c.UserLoginSecurity));
            if (user == null) throw new BusinessException("otpye  uygun user tapilmadi");
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
                IsVerify = false
            };

            return user;
        }
 

    }
}
