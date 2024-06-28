using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Goverment.AuthApi.Services.Dtos.Response.Auth;
using Goverment.Core.CrossCuttingConcers.Resposne.Success;
using Goverment.Core.Security.JWT;

namespace Goverment.AuthApi.Business.Abstracts;

public interface IAuthService
{
    Task<IResponse> Register(CreateUserRequest createUserRequest);
    Task<IDataResponse<Tokens>> Login(UserLoginRequest userLoginRequest);
    Task<IResponse> VerifyAccount(VerifyingRequest verifyOtpCodeRequest);
    Task<IResponse> ReSendOTP(UserEmailRequest emailRequest);

    Task<IResponse> ResetPassword(ResetUserPasswordRequest resetUserPasswordRequest,string idToken);

    Task<IDataResponse<string>> OtpIsTrust(VerifyingRequest verifyingRequest);
    Task<IDataResponse<AccesTokenResponse>> LoginWithRefreshToken(RefreshTokenRequest tokenRequest);

}
