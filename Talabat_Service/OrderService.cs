using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core;
using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Core.ServiceInterfaces;
using Talabat_Core.Specification.OrderSpecifications;

namespace Talabat_Service
{
    public class OrderService : IOrderRepo
    {
        private readonly IBasketRepo _basketRepo;
        //private readonly IGenericIcs<Product> _productRepo;
        //private readonly IGenericIcs<DeliveryMethod> _deliveryrepo;
        //private readonly IGenericIcs<Order> _orderRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _payment;

        public OrderService(IBasketRepo basketRepo,IGenericIcs<Product> productRepo,IUnitOfWork unitOfWork,IPaymentService payment)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _payment = payment;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //1.Get Basket REPO
            var basket =await _basketRepo.GetBasketAsync(basketId);


            //2.Get Selected Items at Basket from Products Repo
            var orderItems=new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach(var basketItems in basket.Items)
                {
                    var product =await _unitOfWork.Repo<Product>().GetAsync(basketItems.Id);
                    var productItemOrdered = new ProductItemOrder(basketItems.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, basketItems.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            //3.Calculate SubTotal
            var subTotal = orderItems.Sum(orderItems=>orderItems.Price*orderItems.Quantity);
            //4.Get DeliverMethods from deliverymethods repo
            var deliveryMethod =await _unitOfWork.Repo<DeliveryMethod>().GetAsync(deliveryMethodId);

            //5.create Order
            var spec =new OrderWithPaymentIntentSpec(basket.PaymentIntentId);
            var ExOrder =await _unitOfWork.Repo<Order>().GettWithSpecAsync(spec);
            if(ExOrder is not null)
            {
                _unitOfWork.Repo<Order>().DeleteAsync(ExOrder);
               await _payment.CreateOrUpdatePaymentIntent(basketId);
            }
            var order=new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal,basket.PaymentIntentId);
            await _unitOfWork.Repo<Order>().AddAsync(order);

            //6.Save to database
          var result=await  _unitOfWork.CompleteAsync();
            if (result <= 0)
            {
                return null;
            }
            else
            {
                return order;
            }

        }

        public async Task<Order> CreateOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo=_unitOfWork.Repo<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var order = await orderRepo.GettWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repo<Order>();
            var spec = new OrderSpecifications(buyerEmail);
            var orders=await orderRepo.GettAllWithSpecAsync(spec);
            return orders;

        }
    }
}
