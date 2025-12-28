using ECommerce.Service.Abstraction;
using ECommerce.Shared.Dtos.BasketDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController:ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string basketId)
        {
            var result = await _basketService.GetBasketAsync(basketId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket([FromBody] BasketDto basket)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string basketId)
        {
            var result = await _basketService.DeleteBasketById(basketId);
            return Ok(result);

        }
    }
}
