using Goverment.Core.CrossCuttingConcers.Exceptions;
using Goverment.Core.CrossCuttingConcers.Resposne.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using System.Net;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
{
    public async Task Invoke(HttpContext context)
    {
		try
		{
			await next(context);
            await SetErrorModelWhenStatusCodeIs401Or403(context);
        }
		catch (Exception exception)
		{
			await HandleExceptionAsync(context, exception);
		}
	}

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
         context.Response.ContentType = "application/json";
        if (exception.GetType() == typeof(BusinessException)) return CreateBusinessException(context, exception);
        if (exception.GetType() == typeof(AuthorizationException))  return CreateAuthorizationException(context, exception);
        if (exception.GetType() == typeof(UnVerifyOrDuplicatedException))  return CreateUnVerifyException(context, exception);
        return exception.GetType() == typeof(ForbiddenException) ? CreateForbiddenException(context, exception) : CreateInternalException(context, exception);
    }

    private Task CreateForbiddenException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Forbidden);
        return context.Response.WriteAsync(new ErrorResponse { Message = exception.Message }.ToString());
    }

    private  Task CreateUnVerifyException(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Conflict);
        return context.Response.WriteAsync(new ErrorResponse { Message = ex.Message}.ToString());
    }

    private Task CreateAuthorizationException(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);
        return context.Response.WriteAsync(new ErrorResponse { Message = ex.Message }.ToString());
    }

    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        return context.Response.WriteAsync(new ErrorResponse { Message = exception.Message }.ToString());
    }


    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
       LogPostgreSQL(exception);
        return context.Response.WriteAsync(new ErrorResponse { Message = "Sistemde Xeta bas verdi" }.ToString());
    }

    private async Task SetErrorModelWhenStatusCodeIs401Or403(HttpContext context)
    {
        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(new ErrorResponse { Message = "forbidden error" }.ToString());
        }
        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(new ErrorResponse().ToString());
        }

    }

    /*  private void LogErrorToDataBase(Exception exception)
      {

          Log.Logger = new LoggerConfiguration()
                 .WriteTo.MSSqlServer(
                     connectionString: configuration.GetConnectionString("AuthUsers"),
                     tableName: "ErrorLog",
                     appConfiguration: configuration,
                     autoCreateSqlTable: true,
                     columnOptionsSection: configuration.GetSection("Serilog"))
                 .CreateLogger();

          Log.Error(exception, exception.Message);
          Log.CloseAndFlush();


      }*/


    private void LogPostgreSQL(Exception exception)
    {

        IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{

    {"message", new RenderedMessageColumnWriter() },
    {"message_template", new MessageTemplateColumnWriter() },
    {"raise_date", new TimestampColumnWriter() },
    {"exception", new ExceptionColumnWriter() },
    {"level", new LevelColumnWriter() },
    {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Text) },
};

        Log.Logger = new LoggerConfiguration()
                            .WriteTo.PostgreSQL
                            (configuration.GetConnectionString("AuthUsers"), "ErrorLog", columnWriters,
                            needAutoCreateTable: true,
                            respectCase: true,
                            useCopy: false)
                            .CreateLogger();

        Log.Error(exception, exception.Message);
        Log.CloseAndFlush();
    }


    /* void LogErrorToFile(Exception exception)
     {

         FileLogConfiguration logConfig = configuration.GetSection(FileLogConfiguration.Connection)
                                                       .Get<FileLogConfiguration>() ??
                                          throw new Exception("File settings is fails");

         string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, ".txt");

         Log.Logger = new LoggerConfiguration()
                  .WriteTo.File(
                          logFilePath,
                          rollingInterval: RollingInterval.Day,
                          retainedFileCountLimit: null,
                          fileSizeLimitBytes: 5000000,
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                  .CreateLogger();

         Log.Error(exception, exception.Message);
         Log.CloseAndFlush();
     }*/

}