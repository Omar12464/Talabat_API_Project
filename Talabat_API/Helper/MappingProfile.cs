using AutoMapper;
using Talabat_API.DTOs;
using Talabat_Core.Models;
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
            CreateMap<CuatomreBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
        }
    }
}
