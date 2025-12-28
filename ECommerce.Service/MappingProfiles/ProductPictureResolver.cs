using AutoMapper;
using ECommerce.Domin.Model.ProductModel;
using ECommerce.Shared.Dtos.productDtos;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Service.MappingProfiles
{
    public class ProductPictureResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureResolver(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            if (source.PictureUrl.StartsWith("http" )|| source.PictureUrl.StartsWith("https")) return source.PictureUrl;
            var baseUrl = _configuration.GetSection("URLS")["BaseUrl"];
            var PictureUrl =$"{baseUrl}{source.PictureUrl}";
            return PictureUrl;
        }
    }
}
