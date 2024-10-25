using AutoMapper;
using Talabat_API.DTOs;
using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;

namespace Talabat_API.Helper
{
    public class OrderItemPictureURLResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.productItem.ProductUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}{source.productItem.ProductUrl}";
            }
            return string.Empty;
        }
    }
}
