using Core.Security.JWT;
using Serilog.Context;

namespace Goverment.AuthApi.Commans.Log.ColumnWriter
{
    public static class HttpContextExtension
    {
        public static void  SetUsernameSerilogContext(this IApplicationBuilder application)
        {
            application.UseMiddleware<UsernameSerilogMiddleware>();
        }
    }

    public class UsernameSerilogMiddleware(RequestDelegate next, IServiceProvider serviceResolver)
    {
        public async Task Invoke(HttpContext context)
        {
            using (var scope = serviceResolver.CreateScope())
            {
                var tokenHelper = scope.ServiceProvider.GetService<ITokenHelper>();
                var username = tokenHelper?.GetUsername();
                LogContext.PushProperty(Config.Username, !string.IsNullOrEmpty(username) ? username : null);
            }
            await next(context);
        }
    }


}


