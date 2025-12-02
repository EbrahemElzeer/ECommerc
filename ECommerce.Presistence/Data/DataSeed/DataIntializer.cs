using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model;
using ECommerce.Domin.Model.ProductModel;
using ECommerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Data.DataSeed

{
    public class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;
        

        public DataIntializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
         
        }
        public async Task IntializeAsync()
        {
            try
            {

                var hasProducts = await _dbContext.Products.AnyAsync();
                var hasBrands = await _dbContext.ProductBrands.AnyAsync();
                var hasTypes = await _dbContext.ProductTypes.AnyAsync();

                if (hasProducts && hasBrands && hasTypes)
                {
                    return;
                }

                if (!hasBrands)
                {
                    await SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }

                if (!hasTypes)
                {
                    await SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);

                }
                _dbContext.SaveChanges();
                if (!hasProducts)
                {
                    await SeedDataFromJson<Product, int>("products.json", _dbContext.Products);
                    await _dbContext.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Occured During Data Intialization {ex}");
            }
        }
        private async Task SeedDataFromJson<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var filePath = @"../ECommerce.Presistence\Data\DataSeed\JsonFiles\" + fileName;

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File Not Found", filePath);
            }

            try
            {
                using var dataStream = File.OpenRead(filePath);

                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data is not null)
                {
                    await dbset.AddRangeAsync(data);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Happen WHile Reading Data From Json {ex}");
            }

        }
    }
}
