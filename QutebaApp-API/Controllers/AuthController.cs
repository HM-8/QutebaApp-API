using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System;
using System.Threading.Tasks;

namespace QutebaApp_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("CreateAccountWithEmailAndPassword")]
        public async Task<User> CreateAccountWithEmailAndPassword([FromBody] AuthenticateUserVM authenticateUserVM)
        {
            try
            {
                return await this._authService.CreateAccountWithEmailAndPassword(authenticateUserVM);
            }
            catch (Exception e) { throw e; }

        }
    }
}
