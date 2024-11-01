using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Order_Aggregate;

namespace Talabat_Core.Specification.OrderSpecifications
{
    public class OrderWithPaymentIntentSpec:BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentSpec(string paymentIntentId):base(b=>b.PaymentIntentId==paymentIntentId)
        {
            
        }
    }
}
