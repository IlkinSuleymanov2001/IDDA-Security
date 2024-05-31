namespace Goverment.AuthApi.Business.Dtos.Request.Auth
{
    public class VerifyAccountRequest
    {
        public string  CacheUserId { get; set; }
        public string OtpCode { get; set;}
    }
}
