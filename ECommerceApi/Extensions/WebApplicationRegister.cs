using ECommerce.Domin.Contracts;
using ECommerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceApi.Extensions
{
    public static class WebApplicationRegister
    {
        public static async Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            var PendingMrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMrations.Any())
            {
              await  dbContext.Database.MigrateAsync();
            }
            return app;
        }

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
         await using  var scope = app.Services.CreateAsyncScope();

            var dataIntializer = scope.ServiceProvider.GetRequiredService<IDataIntializer>();
            await dataIntializer.IntializeAsync();
            return app;
        }

    }
}
