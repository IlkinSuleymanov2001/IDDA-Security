namespace Goverment.AuthApi.Business.Dtos.Request.Auth
{
    public class VerifyingRequest
    {
        public string  CacheUserId { get; set; }
        public string OtpCode { get; set;}
    }
}
