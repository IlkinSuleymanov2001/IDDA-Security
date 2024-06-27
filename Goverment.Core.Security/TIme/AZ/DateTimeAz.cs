namespace Goverment.Core.Security.TIme.AZ
{
    public class DateTimeAz
    {
        private static readonly TimeZoneInfo TimeZoneAz = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");

        public static DateTime Now
        {
            get { return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneAz); }
        }

        public static DateTime Today
        {
            get { return Now.Date; }
        }
    }
}
