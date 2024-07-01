using System;
namespace Goverment.Core.Security.TIme
{

    public class Date
    {

        public static DateTime UtcNow => DateTime.UtcNow;
        public static DateTime Now => DateTime.Now;


        public static DateTime UtcToday => UtcNow.Date;
        public static DateTime Today => Now.Date;


    }
}
