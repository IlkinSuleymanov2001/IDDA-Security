using System;

namespace Goverment.Core.Security.TIme.AZ
{
    public class DateTimeAz
    {
        private static readonly TimeZoneInfo TimeZoneAz = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");

        
        public static DateTime Now
        {
            get {

                var azDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneAz);
                return DateTime.SpecifyKind(azDateTime, DateTimeKind.Utc);
            }
        }

        public static DateTime Today
        {
            get { return Now.Date; }
        }

    }
}
