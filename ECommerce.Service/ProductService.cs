using AutoMapper;
using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model.ProductModel;
using ECommerce.Service.Abstraction;
using ECommerce.Service.Specifications.ProductsSpecifications;
using ECommerce.Shared;
using ECommerce.Shared.CommonRespones;
using ECommerce.Shared.Dtos.productDtos;
using ECommerce.Shared.Dtos.ProductDtos;

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
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specifications = new ProductsWithTypeAndBrandSpecification(queryParams);
            var products=  await _unitOfWork.GetRepostory<Product,int>().GetAllAsync(specifications);
            var result= _mapper.Map<IEnumerable<ProductDto>>(products);
            var countSpecifications = new ProductWithCountSpecification(queryParams);
            var countOfReturn=await _unitOfWork.GetRepostory<Product,int>().CountAsync(countSpecifications);
            return new PaginatedResult<ProductDto>(queryParams.PageIndex,queryParams.PageSize,countOfReturn,result);
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

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id)
        {
            var specifications = new ProductsWithTypeAndBrandSpecification(id);
            var product =await _unitOfWork.GetRepostory<Product,int>().GetByIdAsync(specifications);
            if (product == null)
                return Result<ProductDto>.Fail( Error.NotFound("Product Not Found"));
            return Result<ProductDto>.Ok( _mapper.Map<ProductDto>(product));
        }
    }
}
