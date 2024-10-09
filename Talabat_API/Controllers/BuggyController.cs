using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_API.Errors;
using Talabat_Repository.Data;

namespace Talabat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            var product = _storeContext.Products.Find(100);
            if(product == null)
            {
                return NotFound(new APIResponse(404));
            }
            return Ok(product);
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _storeContext.Products.Find(100);
            var prod_dto = product.ToString();
            return Ok(prod_dto);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new APIResponse(400));
        }
        [HttpGet("unauthorized")]
        public ActionResult GetUnAuthorized()
        {
            return Unauthorized(new APIResponse(401));
        }
       
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
