using System.ComponentModel.DataAnnotations;

namespace Talabat_API.DTOs
{
    public class CuatomreBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItemDTO> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
