using System.ComponentModel.DataAnnotations;
using Talabat_Core.Order_Aggregate;

namespace Talabat_API.DTOs
{
    public class OrdeerDTO
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]

        public string BasketId { get; set; }
        [Required]

        public int  DeliveryMethodId { get; set; }
        public AddressDTO ShippingAddress { get; set; }


    }
}
