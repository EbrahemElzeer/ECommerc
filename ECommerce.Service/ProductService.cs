using AutoMapper;
using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model.ProductModel;
using ECommerce.Service.Abstraction;
using ECommerce.Shared.Dtos.productDtos;
using ECommerce.Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public class ProductService : IProductService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
         var products=  await _unitOfWork.GetRepostory<Product,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
          var Brands = await _unitOfWork.GetRepostory<ProductBrand,int>().GetAllAsync();
            return _mapper.Map <IEnumerable<BrandDto>>(Brands);
            
        }

        public async Task<IEnumerable<TypeDto>> GetALlTypesAsync()
        {
            var Types = await _unitOfWork.GetRepostory<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(Types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product =await _unitOfWork.GetRepostory<Product,int>().GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
