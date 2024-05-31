namespace Goverment.AuthApi.Business.Dtos.Request.Auth
{
    public class VerifyAccoutForResetPassword
    {
        public string  UserId { get; set; }
        public string OtpCode { get; set; }
    }
}
