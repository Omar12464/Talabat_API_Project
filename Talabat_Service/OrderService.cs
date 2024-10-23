﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;
using Talabat_Core.Order_Aggregate;
using Talabat_Core.Repositories_InterFaces;

namespace Talabat_Service
{
    public class OrderService : IOrderRepo
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IGenericIcs<Product> _productRepo;
        private readonly IGenericIcs<DeliveryMethod> _deliveryrepo;
        private readonly IGenericIcs<Order> _orderRepo;

        public OrderService(IBasketRepo basketRepo,IGenericIcs<Product> productRepo, IGenericIcs<DeliveryMethod> deliveryrepo,IGenericIcs<Order> OrderRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _deliveryrepo = deliveryrepo;
            _orderRepo = OrderRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            //1.Get Basket REPO
            var basket =await _basketRepo.GetBasketAsync(basketId);


            //2.Get Selected Items at Basket from Products Repo
            var orderItems=new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach(var basketItems in basket.Items)
                {
                    var product =await _productRepo.GetAsync(basketItems.Id);
                    var productItemOrdered = new ProductItemOrder(basketItems.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, basketItems.Quantity);
                    orderItems.Add(orderItem);
                }
            }
            //3.Calculate SubTotal
            var subTotal = orderItems.Sum(orderItems=>orderItems.Price*orderItems.Quantity);
            //4.Get DeliverMethods from deliverymethods repo
            var deliveryMethod =await _deliveryrepo.GetAsync(deliveryMethodId);
            //5.create Order
            var order=new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal);
            await _orderRepo.AddAsync(order);

            //6.Save to database
            _

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
