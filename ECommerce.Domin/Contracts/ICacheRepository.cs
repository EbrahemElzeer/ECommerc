using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string caheKey);
        Task SetAsync(string caheKey, string caheValue, TimeSpan timeToLive );
    }
}
