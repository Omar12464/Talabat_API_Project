using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;

namespace Talabat_API.DTOs
{
    public class OrderToReturnDTO
    {
        public int iD { get; set; }
        public string BuyerEmail { get; set; }
            public DateTimeOffset OrderDate { get; set; } 
            public string Status { get; set; }
            public Address ShippingAddress { get; set; }
            //public int? DeliveryMethodId { get; set; }
            public string deliveryMethod { get; set; }
            public decimal deliveryMethodCost { get; set; }
            public ICollection<OrderItemDTO> Items { get; set; }
            public decimal SubTotal { get; set; }//OrderPrice without Delivery Method
            public decimal Total { get; set; }//SubTotal+Delivery Method

            public string PaymentIntentId { get; set; }=string.Empty;



    }

}
