using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code) //IActionResult is a method, code is a Error status code
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}