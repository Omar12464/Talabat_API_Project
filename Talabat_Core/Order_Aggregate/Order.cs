using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;

namespace Talabat_Core.Order_Aggregate
{
    public class Order:ModelBase
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, OrderStatus status, Address shippingAddress, DeliveryMethod _deliveryMethod, ICollection<OrderItem> items, decimal subTotal, decimal total)
        {
            BuyerEmail = buyerEmail;
            Status = status;
            ShippingAddress = shippingAddress;
            deliveryMethod = _deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            Total = total;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        //public int? DeliveryMethodId { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }//OrderPrice without Delivery Method
        public decimal Total { get; }//SubTotal+Delivery Method

        public decimal GetTotal()
        {
            return SubTotal + Total;
        }
        public string PaymentIntentId { get; set; }
    }
}
