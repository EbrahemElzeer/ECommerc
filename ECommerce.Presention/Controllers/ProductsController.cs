using ECommerce.Presentation.Attribuites;
using ECommerce.Service.Abstraction;
using ECommerce.Shared;
using ECommerce.Shared.Dtos.productDtos;
using ECommerce.Shared.Dtos.ProductDtos;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [RedisCache(60)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var result = await _productService.GetAllProductsAsync(queryParams);
            return Ok(result);


        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var result= await  _productService.GetProductByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrand()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllType()
        {
            var result = await _productService.GetALlTypesAsync();
            return Ok(result);
        }
    }
}
