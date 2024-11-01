using Microsoft.Extensions.Configuration;
using Stripe;
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
using Product = Talabat_Core.Models.Product;

namespace Talabat_Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepo _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepo basketRepo,IUnitOfWork unitOfWork)
        {
           _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            // Get Secret Key
            StripeConfiguration.ApiKey = _configuration["StripeKeys:SecretKey"];
            // Get Basket
            var basket=await _basketRepo.GetBasketAsync(basketId);
            if(basket == null) { return null; }
            var shippingPrice = 0M;
            if (basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod=await _unitOfWork.Repo<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = DeliveryMethod.Cost;
            }
            // Get Total Price= Total=SubTotal+DM Cost
            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product=await _unitOfWork.Repo<Product>().GetAsync(item.Id);
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }
            var subtotal = basket.Items.Sum(s => s.Price * s.Quantity);
            //Create Payment Intent

            var service=new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                { 
                    Amount =(long) (subtotal*100+shippingPrice*100),
                    Currency="usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                    
                    
                };
                paymentIntent=await service.CreateAsync(option);
                basket.PaymentIntentId=paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)subtotal * 100 + (long)shippingPrice * 100
                };
                paymentIntent =await service.UpdateAsync(basket.PaymentIntentId,options); 
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepo.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
