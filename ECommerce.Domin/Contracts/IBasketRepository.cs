using ECommerce.Domin.Model.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan TimeToLive=default);
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<bool> DeleteBasketById(string basketId);
    }
}
