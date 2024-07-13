using Core.Security.JWT;
using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace Goverment.AuthApi.Commans.Log.ColumnWriter
{
    public class UserNameColumnWriter(): ColumnWriterBase(NpgsqlDbType.Varchar)
    {
        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var (_,value) = logEvent.Properties.FirstOrDefault(c=>c.Key==Config.Username);
            return value?.ToString() ?? null;
        }
    }
}
