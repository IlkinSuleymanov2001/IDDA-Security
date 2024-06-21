namespace Goverment.AuthApi.Business.Dtos.Request.Auth
{

    public class ResetUserPasswordRequest
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
