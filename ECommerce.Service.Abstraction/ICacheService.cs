using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction
{
    public interface ICacheService
    {
        Task<string?>GetAsync(string Cachekey);
        Task SetAsync(string Cachekey,object cachValue,TimeSpan TimeToLive);
    }
}
