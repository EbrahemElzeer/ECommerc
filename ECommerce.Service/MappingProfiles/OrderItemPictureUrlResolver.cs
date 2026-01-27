using AutoMapper;
using ECommerce.Domin.Model.OrderModule;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            if(source.Product.PictureUrl.StartsWith("http")||source.Product.PictureUrl.StartsWith("https"))
            {
                return source.Product.PictureUrl;
            }
            var baseUrl = _configuration.GetSection("ApiSettings:BaseUrl").Value;
            if(string.IsNullOrEmpty(baseUrl))
            {
                return source.Product.PictureUrl;
            }
            return $"{baseUrl}{source.Product.PictureUrl}";
        }
    }
}
