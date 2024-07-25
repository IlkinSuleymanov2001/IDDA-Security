using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing.MailKitImplementations;
using Core.Security.Entities;
using Goverment.AuthApi.Commans.Constants;
using Goverment.AuthApi.Repositories.Abstracts;
using Goverment.Core.Security.TIme;

namespace Goverment.AuthApi.Services.Concretes
{
    public class UserSecurityService(IUserRepository userRepository)
    {
        public  User CheckUserBlock(User user)
        {
            if (!user.UserLoginSecurity.IsAccountBlock) return user;
            var endBlockTime = user.UserLoginSecurity.AccountUnblockedTime ?? Date.UtcNow;
            var minute = (int)(endBlockTime - Date.UtcNow).TotalMinutes;
            if (minute > 0)
                throw new BusinessException("Həddindən çox giriş cəhdi. Bir müddət sonra yenidən yoxlayın");

            user.UserLoginSecurity.IsAccountBlock = false;
            ClearIfRetryCountMax(user);
            return user;
        }



        public  void SendWarningMessage(User user)
        {
            if (user.UserLoginSecurity.LoginRetryCount is 2)
                Gmail.SendWarningMessage(user);
        }

        public  async Task LoginLimitExceed(User user)
        {
            switch (user.UserLoginSecurity.LoginRetryCount)
            {
                case 4:
                    user.UserLoginSecurity.IsAccountBlock = true;
                    user.UserLoginSecurity.AccountBlockedTime = Date.UtcNow;
                    user.UserLoginSecurity.AccountUnblockedTime = DateTime.UtcNow.AddMinutes(15);
                    break;
                case 9:
                    user.UserLoginSecurity.IsAccountBlock = true;
                    user.UserLoginSecurity.AccountBlockedTime = Date.UtcNow;
                    user.UserLoginSecurity.AccountUnblockedTime = DateTime.UtcNow.AddHours(1);
                    break;
                case 14:
                    user.UserLoginSecurity.IsAccountBlock = true;
                    user.UserLoginSecurity.AccountBlockedTime = Date.UtcNow;
                    user.UserLoginSecurity.AccountUnblockedTime = DateTime.UtcNow.AddDays(1); ;
                    break;
            }

            user.UserLoginSecurity.LoginRetryCount += 1;
            await userRepository.UpdateAsync(user);
        }

        public void UnblockUser(User user) 
        {
            user.UserLoginSecurity.LoginRetryCount = 0;
        }

        public  void CheckIdTokenExpireTime(User user)
        {
            if (user.IDTokenExpireDate < Date.UtcNow) throw new BusinessException(Messages.IDTokenExpired);
        }

        private  void ClearIfRetryCountMax(User user)
        {
            if (user.UserLoginSecurity.LoginRetryCount is 15)
                user.UserLoginSecurity.LoginRetryCount = 0;
        }
    }
}
