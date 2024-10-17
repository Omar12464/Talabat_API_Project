using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_API.DTOs;
using Talabat_API.Errors;
using Talabat_Core.Models;
using Talabat_Core.Repositories_InterFaces;

namespace Talabat_API.Controllers { 

    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepo basketRepo,IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {
             var basket=await _basketRepo.GetBasketAsync(Id);
            return Ok(basket?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CuatomreBasketDTO basket)
        {
            var mappedBasket=_mapper.Map<CuatomreBasketDTO,CustomerBasket>(basket);
            var createorupdatedbasket = await _basketRepo.UpdateBasketAsync(mappedBasket);
            if (createorupdatedbasket == null)
            {
                return BadRequest(new APIResponse(400));
            }
            return Ok(createorupdatedbasket);
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepo.DeleteBasketAsync(id);

        }
    }
}
