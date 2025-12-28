using ECommerce.Domin.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;

        public CacheRepository(IConnectionMultiplexer multiplexer)
        {
            _database = multiplexer.GetDatabase();
        }
        public async Task<string?> GetAsync(string caheKey)
        {
            var cacheevalue = await _database.StringGetAsync(caheKey);
          return cacheevalue.IsNullOrEmpty?null: cacheevalue.ToString();
        }

        public async Task SetAsync(string caheKey, string caheValue, TimeSpan timeToLive)
        {
            var cache= await _database.StringSetAsync(caheKey, caheValue,timeToLive);

        }
    }
}
