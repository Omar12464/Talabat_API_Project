using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;

namespace Talabat_Core.Order_Aggregate
{
    public  class OrderItem:ModelBase
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder productItem, decimal price, int quantity)
        {
            this.productItem = productItem;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder productItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
