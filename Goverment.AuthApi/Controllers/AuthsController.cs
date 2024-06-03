using Goverment.AuthApi.Business.Abstracts;
using Goverment.AuthApi.Business.Dtos.Request.Auth;
using Goverment.AuthApi.Business.Dtos.Request.User;
using Goverment.AuthApi.Conifgs;
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
    public async Task<IActionResult> Register(CreateUserRequest createUserRequest)
    {
        var user = await _authService.Register(createUserRequest);
        return Created("", user);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest loginRequest)
    {
        var tokens = await _authService.Login(loginRequest);
        return Ok(tokens);
    }

    [HttpPost("logout")]
    [AuthorizeRoles(Roles = Roles.USER)]
    public async Task Logout() =>
        await _authService.Logout();


    [HttpPost("verifyaccount")]
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyingRequest verifyOtpCodeRequest)
    {
        await _authService.VerifyAccount(verifyOtpCodeRequest);
        return Ok("successfully verify account");
    }

    [HttpPost("regenerateotp")]
    public async Task<IActionResult> ReGenerateOTP(string userId)
    {
        await _authService.ReGenerateOTP(userId);
        return Ok("new opt code send to gmail ");

    }

    [HttpPost("forgetpassword")]
    public async Task<IActionResult> ForgetPassword([FromBody] string email)
    {
        string userId = await _authService.ForgetPassword(email);
        return Ok(userId);

    }

    [HttpPost("verifyotpforresetpassword")]
    public async Task<IActionResult> VerifyOtpResetPassword([FromBody] VerifyingRequest verifyOtpCodeRequest)
    {
        await _authService.VerifyOTPForResetPassword(verifyOtpCodeRequest);
        return Ok("otp dogrulandi");

    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetUserPasswordRequest resetUserPassword)
    {
        await _authService.ResetPassword(resetUserPassword);
        return Ok("passwordunuz dogrulandi");

    }

}
