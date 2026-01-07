using ECommerce.Domin.Contracts;
using ECommerce.Presistence.Data.DbContexts;
using ECommerce.Presistence.IdentityData.DataSeeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            var dataIntializer = scope.ServiceProvider. GetRequiredKeyedService<IDataIntializer>("Default");
            await dataIntializer.IntializeAsync();
            return app;
        }
        public static async Task<WebApplication> SeedIdentityDataAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dataIntializer = scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Identity");
            await dataIntializer.IntializeAsync();
            return app;
        }

    }
}
