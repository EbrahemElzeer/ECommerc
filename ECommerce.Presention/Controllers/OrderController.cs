using ECommerce.Service.Abstraction;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    public class OrderController:ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder( OrderDto orderDto)
        {
          

            var result = await _orderService.CreateOrderAsync(orderDto, GetEmailFRomToken());
            return HandleResult(result);
        }

        [Authorize]
        [HttpGet]
        public async Task <ActionResult<IEnumerable<OrderToReturnDto>>> GetOrders()
        {
            
            var result = await _orderService.GetAllOrderAsync( GetEmailFRomToken()!);
            return HandleResult(result);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOeder(Guid id)
        {
            var result = await _orderService.GetOrderByIdAsync(GetEmailFRomToken(),id);
            return HandleResult(result);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var result = await _orderService.GetAllDeliveryMethodAsync();
            return HandleResult(result);
        }
    }
}
