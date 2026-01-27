using AutoMapper;
using ECommerce.Domin.Model.OrderModule;
using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAdderess>().ReverseMap();
            CreateMap<Order,OrderToReturnDto>()
                .ForMember(dest=>dest.DeliveryMethod,opt=>opt.MapFrom(Src=>Src.DeliveryMethod.ShortName));
        
        CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
        }
    }
}
