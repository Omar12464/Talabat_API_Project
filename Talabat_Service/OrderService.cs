using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Order_Aggregate;
using Talabat_Core.Repositories_InterFaces;

namespace Talabat_Service
{
    public class OrderService : IOrderRepo
    {
        public Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethod, Address shippingAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
