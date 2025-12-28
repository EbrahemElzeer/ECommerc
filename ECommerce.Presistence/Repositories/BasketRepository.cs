using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan TimeToLive = default)
        {
            var json = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, json, (TimeToLive == default) ? TimeSpan.FromDays(7) : TimeToLive);
            if (isCreatedOrUpdated)
            {
                var Basket = await _database.StringGetAsync(basket.Id);
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!)!;
            }
            else return null;

        }

        public async Task<bool> DeleteBasketById(string basketId)
        {
           return  await _database.KeyDeleteAsync(basketId);
          
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket=await _database.StringGetAsync(basketId);
            if(basket.IsNullOrEmpty)
                return null;
            return  JsonSerializer.Deserialize<CustomerBasket?>(basket!);
          
        }
    }
}