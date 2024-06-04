using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Business.Dtos.Response.User;
using Goverment.Core.Security.JWT;

namespace Goverment.AuthApi.Business.Abstracts;

public interface IAuthService
{
    Task Register(CreateUserRequest createUserRequest);
    Task<Token> Login(UserLoginRequest userLoginRequest);
    Task Logout();
    Task VerifyAccount(VerifyingRequest verifyOtpCodeRequest);

    Task ReGenerateOTP(string  email);

    Task ForgetPassword(string email);

    Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest);

}
