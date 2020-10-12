using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.ViewModels;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("getTokenInfo")]
        public async Task<ActionResult<GeneralUserVM>> GetTokenInfo([FromForm] string token)
        {
            FirebaseToken firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

            UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(firebaseToken.Uid);

            GeneralUserVM generalUserVM = new GeneralUserVM()
            {
                UID = user.Uid,
                Name = user.DisplayName,
                Email = user.Email,
                Claims = user.CustomClaims
            };

            return new JsonResult(generalUserVM);
        }
    }
}
