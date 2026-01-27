using ECommerce.Shared.CommonRespones;
using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Abstraction
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email);

        Task<Result<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethodAsync();
        Task<Result<IEnumerable<OrderToReturnDto>>> GetAllOrderAsync(string email);
        Task<Result<OrderToReturnDto>> GetOrderByIdAsync(string email, Guid id);
    }
}
