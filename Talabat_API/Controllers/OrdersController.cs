using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
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
        [ProducesResponseType(typeof(Talabat_Core.Order_Aggregate.Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrdeerDTO orderDto)
        {
           var map= _mapper.Map<AddressDTO, Address>(orderDto.ShippingAddress);
           var order= await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId,map);
            if (order is null) return BadRequest(new APIResponse(400));
            else return Ok(_mapper.Map<OrderToReturnDTO>(order));
        }
        [HttpGet()] // Explicit route to differentiate the two methods
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrderForUser(string email)
        {
            var orders = await _orderService.GetOrderForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderForUser(int id, string email)
        {
            var order = await _orderService.CreateOrderByIdForUserAsync(id, email);
            if (order is null) return NotFound(new APIResponse(404));
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDTO>>(order));
        }

    }
}
