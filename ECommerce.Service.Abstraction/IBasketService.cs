using ECommerce.Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction
{
    public interface IBasketService
    {
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);
        Task<BasketDto?> GetBasketAsync(string basketId);
        Task<bool> DeleteBasketById(string basketId);
    }
}
