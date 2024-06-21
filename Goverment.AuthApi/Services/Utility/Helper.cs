using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Microsoft.Extensions.Primitives;
using System.Linq.Dynamic.Core.Tokenizer;


namespace Goverment.AuthApi.Business.Utlilities
{
    public  static class Helper
    {

        public const string imagesPath = "Business\\Utility\\Photos\\";

        public static  string getCacheJsonId() => Guid.NewGuid().ToString();

        public static int getSeconds(int minute) => minute * 60;
        public const int OtpMaxCount = 2;


        public static  void CheckOtpAndTime(User user, string otpCode, int verifyOtpTime=3)
        {
            if (user.OtpCode != otpCode.Trim()) throw new BusinessException("otp kod duzgun deyil");
            TimeSpan difference = DateTime.Now - (user.OptCreatedDate ?? (DateTime.Now.AddMinutes(0)));
            if ((int)difference.TotalSeconds > getSeconds(verifyOtpTime)) throw new BusinessException("opt kodun vaxdi bitmisdir");

        }
        public static void CheckIDTokenExpireTime(User user)
        {
            if (user.IDTokenExpireDate < DateTime.UtcNow) throw new BusinessException("IDtoken expired");
        }

        public static string GetToken(IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];
            
            if (authorizationHeader != StringValues.Empty) 
            {
                /*if (authorizationHeader.Contains(","))
                    authorizationHeader = authorizationHeader.ToList().FirstOrDefault().Split(",");*/

                string? jwtHeader = authorizationHeader.ToList().Where(c => c.Contains("Bearer")).FirstOrDefault();
                return jwtHeader!=null ? jwtHeader.Split("Bearer").Last().Trim()  : string.Empty;
            }
                
            return string.Empty; ;
        }


    }
}
