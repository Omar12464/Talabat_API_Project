using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_API.DTOs;
using Talabat_API.Errors;
using Talabat_Core.Order_Aggregate;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Service;

namespace Talabat_API.Controllers
{

    public class OrdersController : BaseAPIController
    {
        private readonly IOrderRepo _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepo orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrdeerDTO orderDto)
        {
           var map= _mapper.Map<AddressDTO, Address>(orderDto.ShippingAddress);
           var order= await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId,map);
            if (order is null) return BadRequest(new APIResponse(400));
            else return Ok(order);
        }
    }
}
