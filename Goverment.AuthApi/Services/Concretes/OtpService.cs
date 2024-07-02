﻿using Core.CrossCuttingConcerns.Exceptions;
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
                else
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
            if (user.UserResendOtpSecurity is null) return;
            user.UserResendOtpSecurity.IsLock = false;
            user.UserResendOtpSecurity.TryOtpCount = 0;

        }

        public  void CheckOtpTime(User? user, int verifyOtpTime = 7)
        {
            if (user is null) throw new BusinessException(Messages.InvalidOtp);

            TimeSpan difference = Date.UtcNow - (user.OptCreatedDate ?? (Date.UtcNow.AddMinutes(0)));
            if ((int)difference.TotalSeconds > getSeconds(verifyOtpTime)) throw new BusinessException(Messages.InvalidOtp); ;
        }

        public User GenerateOtp(User user)
        {
            Random rand = new Random();
            var otp = rand.Next(100000, 999999).ToString(); // Generate a random 6-digit number
            user.OtpCode = otp;
            user.OptCreatedDate = Date.UtcNow;
            return user;
        }

        private int getSeconds(int minute) => minute * 60;


    }
}
