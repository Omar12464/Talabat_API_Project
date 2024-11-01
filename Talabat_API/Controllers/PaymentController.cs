using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_API.DTOs;
using Talabat_API.Errors;
using Talabat_Core.Models;
using Talabat_Core.ServiceInterfaces;

namespace Talabat_API.Controllers
{
    public class PaymentController : BaseAPIController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        //Create or update for payment intent
        public PaymentController(IPaymentService paymentService,IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(CuatomreBasketDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<CuatomreBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
           var basket=await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) { return BadRequest(new APIResponse(404,"Basket is not found")); }
            var map = _mapper.Map<CustomerBasket,CuatomreBasketDTO>(basket);
            return Ok(map);
        }
    }
}
