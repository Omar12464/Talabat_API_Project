using AutoMapper;
using Talabat_API.DTOs;
using Talabat_API.Helper;
using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;
using static System.Net.WebRequestMethods;

namespace Talabat_API.ProfileMap
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(b => b.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d=>d.Category,o=>o.MapFrom(c=>c.Category.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureURLResolver_>());
            CreateMap<CuatomreBasketDTO, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDTO, BasketItem>().ReverseMap();
            CreateMap<AddressDTO, Address>();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(D => D.deliveryMethod, o => o.MapFrom(s => s.deliveryMethod.ShortName))
                .ForMember(d => d.deliveryMethodCost, o => o.MapFrom(s => s.deliveryMethod.Cost))  
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()));


            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.productItem.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.productItem.ProductName))
                .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.productItem.ProductUrl))
                .ForMember(d=>d.ProductUrl,o=>o.MapFrom<OrderItemPictureURLResolver>());
            CreateMap<Address, AdressDtoo>().ReverseMap();
        }
    }
}
