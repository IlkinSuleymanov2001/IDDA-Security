using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Goverment.AuthApi.Commans.Constants;
using Goverment.Core.Security.TIme;

namespace Goverment.AuthApi.Services.Concretes
{
    public class OtpService
    {

        public void CheckResendOtpBlock(User user)
        {
            if (user.UserResendOtpSecurity.IsLock)
            {
                if (user.UserResendOtpSecurity.unBlockDate > Date.UtcNow)
                    throw new BusinessException(Messages.ResendOtpError);
                UnBlockResendOtp(user);
            }
            else if (user.UserResendOtpSecurity.TryOtpCount is Constant.ResendOtpMaxCount)
            {
                user.UserResendOtpSecurity.IsLock = true;
                user.UserResendOtpSecurity.unBlockDate = Date.UtcNow.AddHours(2);
            }

            user.UserResendOtpSecurity.TryOtpCount++;

        }
        public  void UnBlockResendOtp(User user)
        {
            user.UserResendOtpSecurity.IsLock = false;
            user.UserResendOtpSecurity.TryOtpCount = 0;

        }

        public  void CheckOtpTime(User user, int verifyOtpTime = 7)
        {
            TimeSpan difference = Date.UtcNow - (user.OptCreatedDate ?? (Date.UtcNow.AddMinutes(0)));
            if ((int)difference.TotalSeconds > GetSeconds(verifyOtpTime)) throw new BusinessException(Messages.InvalidOtp); ;
        }

        public User GenerateOtp(User user)
        {
            var otp = new Random().Next(100000, 999999).ToString(); // Generate a random 6-digit number
            user.OtpCode = otp;
            user.OptCreatedDate = Date.UtcNow;
            return user;
        }

        private int GetSeconds(int minute) => minute * 60;


    }
}
