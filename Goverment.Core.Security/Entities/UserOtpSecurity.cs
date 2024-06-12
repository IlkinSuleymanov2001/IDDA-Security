using Core.Security.Entities;

namespace Goverment.Core.Security.Entities
{
    public  class UserOtpSecurity
    {
        public   int UserId { get; set; }
        public int TryOtpCount { get; set; }
        public DateTime? unBlockDate { get; set; }
        public bool IsLock { get; set; }
        public virtual  User User { get; set; }
    }
}
