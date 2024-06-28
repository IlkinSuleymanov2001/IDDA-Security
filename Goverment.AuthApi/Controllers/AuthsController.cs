using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Services.Dtos.Request.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Goverment.AuthApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]CreateUserRequest createUserRequest)
    {
        return Created("https://govermentauthapi20240610022027.azurewebsites.net/api/Auths/confirm-otp",
            await _authService.Register(createUserRequest));
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
    {
        return Ok(await _authService.Login(loginRequest));
    }


    [HttpPost("confirm-otp")]
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyingRequest verifyOtpCodeRequest)
    {
        return Ok(await _authService.VerifyAccount(verifyOtpCodeRequest));
    }

    [HttpPost("resend-otp")]
    public async Task<IActionResult> ReGenerateOTP([FromBody]UserEmailRequest userEmailRequest)
    {
        return Ok(await _authService.ReSendOTP(userEmailRequest));
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody]UserEmailRequest userEmailRequest)
    {
        ;
        return Ok(await _authService.ReSendOTP(userEmailRequest));

    }


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody]ResetUserPasswordRequest resetUserPassword,[FromQuery]string token)
    {
       ;
        return Ok(await _authService.ResetPassword(resetUserPassword, token));

    }

    [HttpPost("login-refreshtoken")]
    public async  Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest tokenRequest)
    {
        return Ok(await _authService.LoginWithRefreshToken(tokenRequest));
    }

    [HttpPost("otp-trust")]
    public async Task<IActionResult> OtpIsTrust([FromBody] VerifyingRequest verifyingRequest)
    {
        return Ok(await _authService.OtpIsTrust(verifyingRequest));
    }

}
