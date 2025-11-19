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
        private readonly StoreDbContext _storeDbContext;

        public DataIntializer(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task  IntializeAsync()
        {
            try
            {
                var hasProducrts =await  _storeDbContext.Products.AnyAsync();
                var hasProductBrands =await  _storeDbContext.ProductBrands.AnyAsync();
                var hasProductTypes =await _storeDbContext.ProductTypes.AnyAsync();

                if (hasProducrts && hasProductBrands && hasProductTypes)
                {
                    return;
                }

                if (!hasProductBrands)
                {
                    await SeedDataFromJson<ProductBrand, int>("brands.json", _storeDbContext.ProductBrands);
                }
                if (!hasProductTypes)
                {
                     await  SeedDataFromJson<ProductType, int>("types.json", _storeDbContext.ProductTypes);
            }
                _storeDbContext.SaveChanges();

                if (!hasProducrts)
                {
                  await    SeedDataFromJson<Product, int>("products.json", _storeDbContext.Products);
                    _storeDbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error while seeding data to database", ex);
            }
            ;
        }
        private async Task SeedDataFromJson<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var path = @"../ECommerce.Persistence\Data\DataSeed\JsonFiles\" + fileName;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Json file not found", path);
            }

            try
            {
             using   var dataStream = File.OpenRead(path);
                var data=await   JsonSerializer.DeserializeAsync<List<T>>(dataStream,new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive= true
                });
                if(data != null )
                {
                   await dbset.AddRangeAsync(data);
                  
                }

            }catch(Exception ex)
            {
                throw new Exception("Error while reading data form json", ex);
            }


        }
    }
}
