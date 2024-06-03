using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Microsoft.Extensions.Primitives;

namespace Goverment.AuthApi.Business.Utlilities
{
    public  static class Helper
    {

        public const string imagesPath = "Business\\Utility\\Photos\\";

        public static  string getCacheJsonId() => Guid.NewGuid().ToString();

        public static int getSeconds(int minute) => minute * 60;

        public static  void CheckOtpAndTime(User user, string otpCode, int verifyOtpTime=3)
        {
            if (user.OtpCode != otpCode) throw new BusinessException("otp kod duzgun deyil");
            TimeSpan difference = DateTime.Now - (user.OptCreatedDate ?? (DateTime.Now.AddMinutes(-2)));
            if ((int)difference.TotalSeconds > getSeconds(verifyOtpTime)) throw new BusinessException("opt kodun vaxdi bitmisdir");

        }

        public static string GetToken(IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];

            var token = authorizationHeader == StringValues.Empty
            ? string.Empty
            : authorizationHeader.Single().Split(" ").Last();

            return token;
        }
    }
}
