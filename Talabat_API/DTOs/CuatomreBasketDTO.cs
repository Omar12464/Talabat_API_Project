using System.ComponentModel.DataAnnotations;

namespace Talabat_API.DTOs
{
    public class CuatomreBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
    }
}
