using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Order_Aggregate;

namespace Talabat_Core.Repositories_InterFaces
{
    public interface IOrderRepo
    {
        Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        Task<Order> CreateOrderByIdForUserAsync(int orderId, string buyerEmail);


    }
}
