using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;

namespace QutebaApp_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService authService = null;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
    }
}
