using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.Core.CrossCuttingConcers.Results;
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
        await _authService.Register(createUserRequest);
        return Created("https://govermentauthapi20240610022027.azurewebsites.net/api/Auths/confirm-otp", 
            new SuccessResult(message:$"{createUserRequest.FirstName} successfully regsitered,  please verify account "
            ,status:StatusCodes.Status201Created));
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
    {
        var tokens = await _authService.Login(loginRequest);
        return Ok(new SuccessDataResult(tokens));
    }


    [HttpPost("confirm-otp")]
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyingRequest verifyOtpCodeRequest)
    {
        await _authService.VerifyAccount(verifyOtpCodeRequest);
        return Ok(new SuccessResult("successfully verify account"));
    }

    [HttpPost("resend-otp")]
    public async Task<IActionResult> ReGenerateOTP([FromBody]UserEmailRequest userEmailRequest)
    {
        await _authService.ReGenerateOTP(userEmailRequest);
        return Ok(new SuccessResult("new opt code send to gmail"));

    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword([FromBody]UserEmailRequest userEmailRequest)
    {
        await _authService.ReGenerateOTP(userEmailRequest);
        return Ok(new SuccessResult("sended messsage your email"));

    }


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody]ResetUserPasswordRequest resetUserPassword)
    {
        await _authService.ResetPassword(resetUserPassword);
        return Ok(new SuccessResult("passwordunuz dogrulandi"));

    }

    [HttpPost("login-refreshtoken")]
    public async  Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest tokenRequest)
    {
        var accestoken  =  await _authService.LoginWithRefreshToken(tokenRequest);
        return Ok(new SuccessDataResult(data: accestoken));
    }

}
