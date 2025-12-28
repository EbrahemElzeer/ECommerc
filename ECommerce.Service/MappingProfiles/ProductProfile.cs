using AutoMapper;
using ECommerce.Domin.Model.ProductModel;
using ECommerce.Shared.Dtos.productDtos;
using ECommerce.Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile() {

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                
               .ForMember(dest=>dest.ProductBrand,opt=>opt.MapFrom(src=>src.ProductBrand.Name ))
               .ForMember(dest=>dest.ProductType,opt=>opt.MapFrom(src=>src.ProductType.Name))
               .ForMember(dest=>dest.PictureUrl,opt=>opt.MapFrom<ProductPictureResolver>());
       
        }
    }
}
