using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Auth;
using Goverment.Core.Security.JWT;

namespace Goverment.AuthApi.Business.Abstracts;

public interface IAuthService
{
    Task Register(CreateUserRequest createUserRequest);
    Task<Tokens>  Login(UserLoginRequest userLoginRequest);
    Task VerifyAccount(VerifyingRequest verifyOtpCodeRequest);

    Task ReGenerateOTP(UserEmailRequest emailRequest);

    Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest);

    Task<AccesTokenResponse> LoginWithRefreshToken(RefreshTokenRequest tokenRequest);

}
