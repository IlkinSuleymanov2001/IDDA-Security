using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;

namespace Goverment.AuthApi.Business.Abstracts;

public interface IAuthService
{
    Task Register(CreateUserRequest createUserRequest);
    Task<object> Login(UserLoginRequest userLoginRequest);
    Task VerifyAccount(VerifyingRequest verifyOtpCodeRequest);

    Task ReGenerateOTP(UserEmailRequest emailRequest);

    Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest);

}
