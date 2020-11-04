using Microsoft.AspNetCore.Mvc;

namespace QutebaApp_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {

        [HttpGet]
        [Route("v1")]
        public IActionResult Test()
        {
            return Ok();
        }

    }
}
