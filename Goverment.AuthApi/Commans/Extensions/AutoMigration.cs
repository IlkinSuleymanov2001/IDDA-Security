using Goverment.AuthApi.Repositories.Concretes.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Goverment.AuthApi.Commans.Extensions
{
    public static class AutoMigration
    {
        public static async Task<WebApplication> ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuthContext>();
            await db.Database.MigrateAsync();
            return app;
        }
    }
}
