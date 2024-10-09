using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_API.Errors;

namespace Talabat_API.Controllers
{
    [Route("Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            if (code == 401)
            {
                return Unauthorized(new APIResponse(401));
            }
            else if (code == 404)
            {
                return NotFound(new APIResponse(404));
            }
            else
                return StatusCode(code);
        }
    }
}
