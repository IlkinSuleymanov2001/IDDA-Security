using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.Core.Security.JWT;

namespace Goverment.AuthApi.Business.Abstracts;

public interface IAuthService
{
    Task<string> Register(CreateUserRequest createUserRequest);
    Task<Token> Login(UserLoginRequest userLoginRequest);
    Task VerifyAccount(VerifyAccountRequest verifyOtpCodeRequest);
    Task RegisterWithConfirmToken(CreateUserRequest createUserRequest);
    Task VerifyAccount(string verifConfirm);
    Task ReGenerateOTP(string  userId);

    Task<string> ForgetPassword(string email);

    Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest);
    Task VerifyOTPForResetPassword(VerifyAccoutForResetPassword verifyAccoutForReset);

}
