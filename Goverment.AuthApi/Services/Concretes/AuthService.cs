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
using Goverment.AuthApi.Commans.Attributes;
using Goverment.AuthApi.Commans.Constants;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.AuthApi.Services.Concretes;
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Staff;
using Goverment.AuthApi.Services.Http;
using Goverment.Core.CrossCuttingConcers.Exceptions;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.JWT;
using Goverment.Core.Security.TIme;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IUserOtpSecurityRepository _otpResendSecurityRepository;
        private readonly string  _currentUser;
        private readonly OtpService _otpService;
        private readonly UserSecurityService _userSecurityService;
        private readonly IHttpService _httpService;


        public AuthService(ITokenHelper jwtService,
            IUserRepository userRepository, IMapper mapper,
            IUserLoginSecurityRepository loginSecurityRepository,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IUserOtpSecurityRepository otpSecurityRepository,
            OtpService otpService,
            UserSecurityService userSecurityService,
            IHttpService httpService)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _mapper = mapper;
            _loginSecurityRepository = loginSecurityRepository;
            _currentUser = _jwtService.GetUsername();
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _otpResendSecurityRepository = otpSecurityRepository;
            _otpService = otpService;
            _userSecurityService = userSecurityService;
            _httpService = httpService;
        }

        public async Task<IDataResponse<Tokens>> LoginForMobile(UserLoginRequest userLoginRequest)
        {

            var user = await _userRepository.GetAsync(u => u.Email == userLoginRequest.Email,
               include: ef => ef.Include(e => e.UserLoginSecurity).Include(e => e.UserResendOtpSecurity));

            if (user is null) throw new BusinessException(Messages.UserNameAndPasswordError);
            var isTruePassword = HashingHelper.VerifyPasswordHash(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt);

            if (!user.IsVerify) 
            {
                if(!isTruePassword) throw new BusinessException(Messages.UserNameAndPasswordError);
                await ReSendOTP(user);
                throw new UnVerifyException();
            }
            

            _userSecurityService.CheckUserBlock(user);
            if (!isTruePassword)
            {
                _userSecurityService.SendWarningMessage(user);
                await _userSecurityService.LoginLimitExceed(user);
                throw new BusinessException(Messages.UserNameAndPasswordError);
            }

            user.Status = true;

            _userSecurityService.UnblockUser(user);
            _otpService.UnBlockResendOtp(user);

            await _userRepository.UpdateAsync(user);

           var tokens = _jwtService.CreateTokens(user,await GetUserRoles(user));

            return  DataResponse<Tokens>.Ok(tokens);

        }

        public async Task<IDataResponse<PermissionTokens>> LoginForWeb(UserLoginRequest userLoginRequest)
        {
            var user = await _userRepository.GetAsync(u => u.Email == userLoginRequest.Email,
               include: ef => ef.Include(e => e.UserLoginSecurity).Include(e => e.UserResendOtpSecurity)
               .Include(c=>c.UserRoles).ThenInclude(c=>c.Role));

            if (user is null || !user.IsVerify) throw new BusinessException(Messages.UserNameAndPasswordError);

            var roles  =  user.UserRoles.Select(c => c.Role);
            var isUser = roles.Count() == 1 && !roles.Where(c => c.Name == Roles.USER).IsNullOrEmpty();
            if (isUser) throw new BusinessException(Messages.UserNameAndPasswordError);
            _userSecurityService.CheckUserBlock(user);

            var isTruePassword = HashingHelper.VerifyPasswordHash(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt);

            if (!isTruePassword)
            {
                _userSecurityService.SendWarningMessage(user);
                await _userSecurityService.LoginLimitExceed(user);
                throw new BusinessException(Messages.UserNameAndPasswordError);
            }

            user.Status = true;

            _userSecurityService.UnblockUser(user);
            _otpService.UnBlockResendOtp(user);

            await _userRepository.UpdateAsync(user);
            var userroles = await GetUserRoles(user);

            var tokens = _jwtService.CreateTokens(user, userroles);
            var permissons = userroles.Select(c => c.Name).ToArray();
            IDataResponse<StaffResponse> response =default;
            if (permissons.Contains(Roles.STAFF))
            {
                response = await _httpService.GetAsync<DataResponse<StaffResponse>>(
                    url: "https://adminms.azurewebsites.net/api/Staffs/get",
                    token:tokens.RefreshToken);
                tokens = _jwtService.CreateTokens(user, userroles, response?.Data?.OrganizationName);
            }


            return DataResponse<PermissionTokens>.Ok(new PermissionTokens 
            {
             AccessToken = tokens.AccessToken ,
             RefreshToken = tokens.RefreshToken,
             Permissons = permissons,
             Staff = response?.Data
            });
             
        }




        public async Task<IDataResponse<AccesTokenResponse>> LoginWithRefreshToken(RefreshTokenRequest tokenRequest)
        {
            var response = _jwtService.ParseJwtAndCheckExpireTime(tokenRequest.Token);
            if (!response.expire)
                throw new AuthorizationException();

             var user = await _userRepository.GetAsync(predicate:u => u.Email == response.username,
             include: ef => ef.Include(e => e.UserRoles).ThenInclude(u=>u.Role));

            var acccessToken =  new AccesTokenResponse
            {
                Token = _jwtService.CreateTokens(user, await GetUserRoles(user)).AccessToken
            };

            return  DataResponse<AccesTokenResponse>.Ok(acccessToken);

        }


        public async Task<IResponse> Register(CreateUserRequest createUserRequest)
        {
            await EmailIsUniqueWhenUserCreated(createUserRequest.Email);
             var user = await CreateUser(createUserRequest);
            _otpService.GenerateOtp(user);
            await _userRepository.AddAsync(user);
            await _otpResendSecurityRepository.AddAsync(new UserResendOtpSecurity {User = user });
            await _loginSecurityRepository.AddAsync(new UserLoginSecurity { User = user, LoginRetryCount = 0 });
            Gmail.OtpSend(user);
            return new Response("Ugurla qeydiyyatdan kecdiniz, zehmet olmasa mailinizi tesdiqleyin..");

        }

        public async Task<IResponse> VerifyAccount(VerifyingRequest accountRequest)
        {
            User? user = await FindUserByOtp(accountRequest.OtpCode);
            _otpService.CheckOtpTime(user, 3);

            if (user.IsVerify) throw new BusinessException("otp bu emeliyyat ucun uygun deyil");

            user.IsVerify = true;
            user.OtpCode = null;
            user.UserRoles.Add(new UserRole { RoleId = _Role.Id, User = user });
            await _userRepository.UpdateAsync(user);
            return Response.Ok("succesfully verify account");
        }

        public async Task<IResponse> ReSendOTP(UserEmailRequest emailRequest)
        {
            
            var user = await _userRepository.GetAsync(u => u.Email == emailRequest.Email.ToLower(),
                include: ef => ef.Include(e => e.UserLoginSecurity).Include(e => e.UserResendOtpSecurity));
            if (user == null) throw new BusinessException(Messages.UserNotExists);
            await ReSendOTP(user);
            return  Response.Ok();
        }

        private  async Task ReSendOTP(User user)
        {
            _otpService.CheckResendOtpBlock(user);
            _otpService.GenerateOtp(user);
            await _userRepository.UpdateAsync(user);
            Gmail.OtpSend(user);
        }

        public async Task<IDataResponse<string>> OtpIsTrust(VerifyingRequest verifyingRequest)
        {
            User? user = await FindUserByOtp(verifyingRequest.OtpCode);
            _otpService.CheckOtpTime(user,3);

            user.OtpCode = null;
            user.OptCreatedDate = null;
            user.IDToken = _jwtService.IDToken();
            user.IDTokenExpireDate = Date.UtcNow.AddMinutes(7);
            await _userRepository.UpdateAsync(user);
            return  DataResponse<string>.Ok(user.IDToken);
        }

        public async Task<IResponse> ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest, string idToken)
        {

            User? user = await _userRepository.GetAsync(c => c.IDToken == idToken);

            if (user == null) throw new BusinessException(Messages.IDTokenExpired);
            _userSecurityService.CheckIDTokenExpireTime(user);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(resetUserPasswordRequest.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IDToken = null;
            user.IDTokenExpireDate = null;
            await _userRepository.UpdateAsync(user);
            return Response.Ok("password ugurla deyisdirildi..");


        }


        private async Task<IList<Role>> GetUserRoles(User user)
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

        private async Task EmailIsUniqueWhenUserCreated(string email)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException(Messages.EmailIsUnique);
        }

        private async Task<User?> FindUserByOtp(string otp)
        {
            var user = await _userRepository.GetAsync(u => u.OtpCode == otp,
                include:ef=>ef.Include(c=>c.UserLoginSecurity));
            return user;
        }

        private async Task<User> CreateUser(CreateUserRequest createUserRequest)
        {
            await EmailIsUniqueWhenUserCreated(createUserRequest.Email);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(createUserRequest.Password, out passwordHash, out passwordSalt);

            User user = new User
            {
                Email = createUserRequest.Email,
                FullName = createUserRequest.FullName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsVerify = false
            };

            return user;
        }

       
    }
}