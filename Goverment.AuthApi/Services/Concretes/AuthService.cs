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
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Staff;
using Goverment.AuthApi.Services.Http;
using Goverment.Core.CrossCuttingConcers.Exceptions;
using Goverment.Core.CrossCuttingConcers.Resposne.Error;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Goverment.Core.Security.Entities;
using Goverment.Core.Security.JWT;
using Goverment.Core.Security.TIme;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Services.Concretes
{
    public class AuthService(
        ITokenHelper jwtService,
        IUserRepository userRepository,
        IUserLoginSecurityRepository loginSecurityRepository,
        IUserOtpSecurityRepository otpSecurityRepository,
        OtpService otpService,
        UserSecurityService userSecurityService,
        IHttpService httpService) : IAuthService
    {
        public async Task<IDataResponse<Tokens>> LoginForMobile(UserLoginRequest userLoginRequest)
        {

            var user = await userRepository.GetAsync(u => u.Email == userLoginRequest.Email,
            include: ef => ef.Include(e => e.UserLoginSecurity)
                             .Include(e => e.UserResendOtpSecurity)
                             .Include(c => c.UserRoles)
                             .ThenInclude(c => c.Role))
                ?? throw new BusinessException(Messages.UserNameAndPasswordError);

            var isTruePassword = HashingHelper.VerifyPasswordHash(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt);

            if (!user.IsVerify)
            {
                if (!isTruePassword) throw new BusinessException(Messages.UserNameAndPasswordError);
                await ReSendOtp(user);
                throw new UnVerifyOrDuplicatedException();
            }

            userSecurityService.CheckUserBlock(user);
            if (!isTruePassword)
            {
                userSecurityService.SendWarningMessage(user);
                await userSecurityService.LoginLimitExceed(user);
                throw new BusinessException(Messages.UserNameAndPasswordError);
            }

            user.Status = true;
            userSecurityService.UnblockUser(user);
            otpService.UnBlockResendOtp(user);

            var tokens = jwtService.CreateTokens(user, user.UserRoles.Select(c => c.Role).ToList());

            await userRepository.UpdateAsync(user);
            return DataResponse<Tokens>.Ok(tokens);

        }

        public async Task<IDataResponse<PermissionTokens>> LoginForWeb(UserLoginRequest userLoginRequest)
        {
            var user = await userRepository.GetAsync(u => u.Email == userLoginRequest.Email,
               include: ef => ef.Include(e => e.UserLoginSecurity).
                   Include(e => e.UserResendOtpSecurity)
                   .Include(c => c.UserRoles).ThenInclude(c => c.Role));
            if (user == null) throw new BusinessException(Messages.UserNameAndPasswordError);
            var isPasswordTrue = HashingHelper.VerifyPasswordHash(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt);

            if (!user.IsVerify) throw new BusinessException(Messages.UserNameAndPasswordError);
            var roleList = user.UserRoles.Select(c => c.Role).ToList();


            var staffOrAdmin = roleList.Any(c => c.Name is Roles.STAFF or Roles.ADMIN);
            if (!staffOrAdmin && isPasswordTrue) throw new BusinessException("isdifadəçi tapilmadi");

            userSecurityService.CheckUserBlock(user);

            if (!isPasswordTrue)
            {
                userSecurityService.SendWarningMessage(user);
                await userSecurityService.LoginLimitExceed(user);
                throw new BusinessException(Messages.UserNameAndPasswordError);
            }

            user.Status = true;
            userSecurityService.UnblockUser(user);
            otpService.UnBlockResendOtp(user);
            await userRepository.UpdateAsync(user);


            var tokens = jwtService.CreateTokens(user, roleList);
            tokens = await SetOrganizationName(user, tokens, roleList);

            return DataResponse<PermissionTokens>.Ok(new PermissionTokens
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                Permissons = roleList.Select(c => c.Name).ToArray()
            });

        }

        private async Task<Tokens> SetOrganizationName(User user, Tokens tokens, IList<Role> roleList)
        {

            if (!roleList.Select(c => c.Name).Contains(Roles.STAFF)) return tokens;
            var dataResponse = await httpService.GetAsync<HttpResponse<StaffResponse>,ErrorResponse>(url: "https://adminapi20240708182629.azurewebsites.net/api/Staffs/get", token: tokens.AccessToken);
            return jwtService.CreateTokens(user, roleList, new AddtionalParam { Value = dataResponse?.Data?.OrganizationName });

        }


        public async Task<IDataResponse<AccesTokenResponse>> LoginWithRefreshToken(RefreshTokenRequest tokenRequest)
        {
            try
            {
                if (jwtService.CurrentRoleEqualsTo(Roles.STAFF))
                    await httpService.GetAsync<HttpResponse<StaffResponse>, ErrorResponse>
                    (url: "https://adminapi20240708182629.azurewebsites.net/api/Staffs/get",
                     autoToken: true);
            }
            catch(Exception)
            {
                throw new AuthenticationException();
            }


            return jwtService.ValidateToken(tokenRequest.Token)
                ? DataResponse<AccesTokenResponse>.Ok(new AccesTokenResponse
                {
                    Token = jwtService.AddExpireTime(tokenRequest.Token,3)
                })
                : throw new AuthorizationException();
        }


        public async Task<IResponse> Register(CreateUserRequest createUserRequest)
        {
            await EmailIsUniqueWhenUserCreated(createUserRequest.Email);
            var user = await CreateUser(createUserRequest);
            otpService.GenerateOtp(user);
            await userRepository.AddAsync(user);
            await otpSecurityRepository.AddAsync(new UserResendOtpSecurity { User = user, TryOtpCount = 0 });
            await loginSecurityRepository.AddAsync(new UserLoginSecurity { User = user, LoginRetryCount = 0 });
            Gmail.OtpSend(user);
            return Response.Ok("Ugurla qeydiyyatdan kecdiniz, zehmet olmasa mailinizi tesdiqleyin..");
        }

        public async Task<IResponse> VerifyAccount(VerifyingRequest accountRequest)
        {
            var user = await FindUserByOtp(accountRequest.OtpCode);
            otpService.CheckOtpTime(user, 3);

            if (user.IsVerify) throw new BusinessException("hesabiniz artiq tesdiqlenib");

            user.IsVerify = true;
            user.OtpCode = null;
            user.OptCreatedDate = null;
            user.UserRoles.Add(new UserRole { RoleId = ROLE_USER.Id });
            await userRepository.UpdateAsync(user);
            return Response.Ok("succesfully verify account");
        }

        public async Task<IResponse> ReSendOtp(UserEmailRequest emailRequest)
        {
            var user = await userRepository.GetAsync(u => u.Email == emailRequest.Email.ToLower(),
                include: ef => ef.Include(e => e.UserLoginSecurity).Include(e => e.UserResendOtpSecurity))
                 ?? throw new BusinessException(Messages.UserNotExists);
            await ReSendOtp(user);
            return Response.Ok();
        }

        private async Task ReSendOtp(User user)
        {
            otpService.CheckResendOtpBlock(user);
            otpService.GenerateOtp(user);
            await userRepository.UpdateAsync(user);
            Gmail.OtpSend(user);
        }

        public async Task<IDataResponse<string>> OtpIsTrust(VerifyingRequest verifyingRequest)
        {
            var user = await FindUserByOtp(verifyingRequest.OtpCode);
            otpService.CheckOtpTime(user, 3);

            user.OtpCode = null;
            user.OptCreatedDate = null;
            user.IDToken = jwtService.IdToken();
            user.IDTokenExpireDate = Date.UtcNow.AddMinutes(7);
            await userRepository.UpdateAsync(user);
            return DataResponse<string>.Ok(user.IDToken);
        }

        public async Task<IResponse> ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest, string idToken)
        {

            var user = await userRepository.GetAsync(c => c.IDToken == idToken) ?? throw new BusinessException(Messages.IDTokenExpired);
            userSecurityService.CheckIdTokenExpireTime(user);

            HashingHelper.CreatePasswordHash(resetUserPasswordRequest.Password, 
                out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IDToken = null;
            user.IDTokenExpireDate = null;
            await userRepository.UpdateAsync(user);
            return Response.Ok("password ugurla deyisdirildi..");


        }

        private async Task EmailIsUniqueWhenUserCreated(string email)
        {
            var user = await userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException(Messages.EmailIsUnique);
        }

        private async Task<User> FindUserByOtp(string otp)
        {
            return await userRepository.GetAsync(u => u.OtpCode == otp,
                   include: ef => ef.Include(c => c.UserLoginSecurity)) 
                   ?? throw new BusinessException(Messages.InvalidOtp);
        }

        private async Task<User> CreateUser(CreateUserRequest createUserRequest)
        {
            await EmailIsUniqueWhenUserCreated(createUserRequest.Email);
            HashingHelper.CreatePasswordHash(createUserRequest.Password, 
                out var  passwordHash, out var  passwordSalt);
            return new User
            {
                Email = createUserRequest.Email,
                FullName = createUserRequest.FullName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsVerify = false
            };
        }
    }
}