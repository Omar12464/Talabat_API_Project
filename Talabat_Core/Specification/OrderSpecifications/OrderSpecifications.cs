using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Order_Aggregate;

namespace Talabat_Core.Specification.OrderSpecifications
{
    public class OrderSpecifications:BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail):base(o=>o.BuyerEmail==buyerEmail)
        {
            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.Items);
            AddOrderByASC(o => o.OrderDate);

            
        }
        public OrderSpecifications(int id, string buyerEmail):base(o=>o.Id==id&&o.BuyerEmail==buyerEmail)
        {
            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
