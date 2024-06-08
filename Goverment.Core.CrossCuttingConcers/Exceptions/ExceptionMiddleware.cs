using FluentValidation;
using Goverment.Core.CrossCuttingConcers.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using System.Net;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration configuration;

    public ExceptionMiddleware(RequestDelegate next,IConfiguration configuration)
    {
        _next = next;
        this.configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
		try
		{
			await _next(context);
		}
		catch (Exception exception)
		{
			await HandleExceptionAsync(context, exception);
		}
	}

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception.GetType() == typeof(ValidationException)) return CreateValidationException(context, exception);
        if (exception.GetType() == typeof(BusinessException)) return CreateBusinessException(context, exception);
        if (exception.GetType() == typeof(AuthorizationException))
            return CreateAuthorizationException(context, exception);
        return CreateInternalException(context, exception);
    }

    private Task CreateAuthorizationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

        return context.Response.WriteAsync(new CustomProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Type = "https://example.com/probs/authorization",
            Title = "Authorization exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateBusinessException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return context.Response.WriteAsync(new CustomProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/business",
            Title = "Business exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateValidationException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        object errors = ((ValidationException)exception).Errors;

        return context.Response.WriteAsync(new CustomProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        }.ToString());
    }

    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
        //LogErrorToDataBase(exception);
        LogErrorToPostgreDatabase(exception);
        LogErrorToFile(exception);
        return context.Response.WriteAsync(new CustomProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://example.com/probs/internal",
            Title = "Internal exception(Sql,Cod Bomb)",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
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

    private void LogErrorToPostgreDatabase(Exception exception)
    {
       
        IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{
    {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
    {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
    {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) }
};

        Log.Logger = new LoggerConfiguration()
                            .WriteTo.PostgreSQL
                            (configuration.GetConnectionString("AuthUsers"), "ErrorLog", columnWriters,
                            needAutoCreateTable:true,
                            respectCase:true,
                            useCopy:false)
                            .CreateLogger();

        Log.Error(exception, exception.Message);
        Log.CloseAndFlush();
    }


    void LogErrorToFile(Exception exception)
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
    }
    
}