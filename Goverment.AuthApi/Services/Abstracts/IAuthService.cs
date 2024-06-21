using Core.Security.Entities;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Auth;
using Goverment.Core.CrossCuttingConcers.Results;
using Goverment.Core.Security.JWT;

namespace Goverment.AuthApi.Business.Abstracts;

public interface IAuthService
{
    Task Register(CreateUserRequest createUserRequest);
    Task<Tokens>  Login(UserLoginRequest userLoginRequest);
    Task VerifyAccount(VerifyingRequest verifyOtpCodeRequest);

    Task ReGenerateOTP(UserEmailRequest emailRequest);
    Task ReGenerateOTP(User user);

    Task ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest,string idToken);

    Task<DataResult> OtpIsTrust(VerifyingRequest verifyingRequest);

    Task<AccesTokenResponse> LoginWithRefreshToken(RefreshTokenRequest tokenRequest);

}
