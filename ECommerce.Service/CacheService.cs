using ECommerce.Domin.Contracts;
using ECommerce.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _repository;

        public CacheService(ICacheRepository repository)
        {
            _repository = repository;
        }
        public async Task<string?> GetAsync(string Cachekey)
        {
            return await _repository.GetAsync(Cachekey);
        }

        public Task SetAsync(string Cachekey, object cachValue, TimeSpan TimeToLive)
        {
           var value=JsonSerializer.Serialize(cachValue,new JsonSerializerOptions()
           {
               PropertyNamingPolicy=JsonNamingPolicy.CamelCase
           });
            return _repository.SetAsync(Cachekey, value, TimeToLive);
        }
    }
}
