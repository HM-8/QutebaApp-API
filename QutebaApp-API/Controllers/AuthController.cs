using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.OtherModels;
using QutebaApp_Data.ViewModels;
using System;

namespace QutebaApp_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }


        [HttpPost]
        [Route("signup-Email")]
        public IActionResult SignUpEmail([FromBody] AuthenticateUserVM authenticateUser, int pageId)
        {
            try
            {
                string role = null;
                string createdAccountWith = "password";
                if (pageId == (int)PageTypes.RegisterSuperAdmin)
                {
                    role = "superadmin";
                }
                if (pageId == (int)PageTypes.RegisterAdmin)
                {
                    role = "admin";
                }
                if (pageId == (int)PageTypes.RegisterUser)
                {
                    role = "user";
                }

                var authenticatedUser = authService.Register(authenticateUser, role, createdAccountWith);

                var claims = authService.SetCustomClaims(authenticatedUser.ID, role);

                var userDetails = authService.GetToken(authenticatedUser, claims);

                return new JsonResult(userDetails);
            }
            catch (Exception e) { throw e; }
        }

        [HttpPost]
        [Route("signin-Email")]
        public IActionResult SignInEmail([FromBody] AuthenticateUserVM authenticateUser)
        {
            try
            {
                var authenticatedUser = authService.Login(authenticateUser);

                if (authenticatedUser == null) throw new Exception("User is unauthorized or credential does not match");

                var claims = authService.SetCustomClaims(authenticatedUser.ID, authenticatedUser.Role);

                var userDetails = authService.GetToken(authenticatedUser, claims);

                return new JsonResult(userDetails);
            }
            catch (Exception e) { throw e; }
        }

        /*[HttpPost]
        [Route("google")]
        public async Task<ActionResult> Google([FromForm] string token, int pageId)
        {
            try
            {
                string role = null;

                if (pageId == (int)PageTypes.RegisterSuperAdmin)
                {
                    role = "superadmin";
                }
                if (pageId == (int)PageTypes.RegisterAdmin)
                {
                    role = "admin";
                }
                if (pageId == (int)PageTypes.RegisterUser)
                {
                    role = "user";
                }

                *//*var authenticatedUser = await authService.Register(token, role);

                return new JsonResult(authenticatedUser);*//*
            }
            catch (Exception e) { throw e; }
        }*/

    }
}
