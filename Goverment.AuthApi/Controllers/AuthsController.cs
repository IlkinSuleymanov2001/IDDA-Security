using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Goverment.Core.Security.TIme;
using Microsoft.AspNetCore.Mvc;

namespace Goverment.AuthApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]CreateUserRequest createUserRequest)
        =>Created("/api/Auths/confirm-otp",await authService.Register(createUserRequest));



    [HttpPost("login")]
    public async Task<IActionResult> LoginMobile([FromBody] UserLoginRequest loginRequest)
        => Ok(await authService.LoginForMobile(loginRequest));


    [HttpPost("loginweb")]
    public async Task<IActionResult> LoginWeb([FromBody] UserLoginRequest loginRequest)
     => Ok(await authService.LoginForWeb(loginRequest));


    [HttpPost("confirm-otp")]
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyingRequest verifyOtpCodeRequest)
        => Ok(await authService.VerifyAccount(verifyOtpCodeRequest));

    [HttpPost("resend-otp")]
    public async Task<IActionResult> ReGenerateOTP([FromBody]UserEmailRequest userEmailRequest)
        => Ok(await authService.ReSendOTP(userEmailRequest));

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody]UserEmailRequest userEmailRequest)
        => Ok(await authService.ReSendOTP(userEmailRequest));


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody]ResetUserPasswordRequest resetUserPassword,[FromQuery]string token)
        => Ok(await authService.ResetPassword(resetUserPassword, token));


    [HttpPost("login-refreshtoken")]
    public async  Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest tokenRequest)
        => Ok(await authService.LoginWithRefreshToken(tokenRequest));

    [HttpPost("otp-trust")]
    public async Task<IActionResult> OtpIsTrust([FromBody] VerifyingRequest verifyingRequest)
        => Ok(await authService.OtpIsTrust(verifyingRequest));

}
