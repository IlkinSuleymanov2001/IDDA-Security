using Goverment.AuthApi.Commans.Log.ColumnWriter;
using Microsoft.AspNetCore.HttpLogging;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Goverment.AuthApi.Commans.Extensions
{
    public static  class LoggerExtension
    {
        public static IServiceCollection AddLogConfig(this IServiceCollection services,IConfiguration configuration,IHostBuilder builder)
        {
            var dateFormat  =  DateTime.UtcNow.ToString("yyyy-MM-dd");
            var log = new LoggerConfiguration()
                .WriteTo.Console().
                WriteTo.File($"log/{dateFormat}.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: 5000000)
                .WriteTo.PostgreSQL
                (configuration.GetConnectionString("AuthUsers"), "ErrorLog",
                    new Dictionary<string, ColumnWriterBase>
                    {
                        {"message", new RenderedMessageColumnWriter() },
                        {"message_template", new MessageTemplateColumnWriter() },
                        {"raise_date", new TimestampColumnWriter() },
                        {"exception", new ExceptionColumnWriter() },
                        {"level", new LevelColumnWriter() },
                        {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Text) },
                        {"username", new UserNameColumnWriter() }
                    },
                    needAutoCreateTable: true,
                    respectCase: true,
                    useCopy: false)
                .Enrich.FromLogContext().
                MinimumLevel.Error()
                .CreateLogger();

            builder.UseSerilog(log);

           /* services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.CombineLogs = true;
            });*/
            return services;
        }
    }
}
