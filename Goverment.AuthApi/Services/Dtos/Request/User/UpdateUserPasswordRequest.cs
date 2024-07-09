namespace Goverment.AuthApi.Business.Dtos.Request.User
{
    public record UpdateUserPasswordRequest(string CurrentPassword, string Password, string ConfirmPassword);

}

