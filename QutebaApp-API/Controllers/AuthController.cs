using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("google")]
        public IActionResult Google([FromForm] string tokenId)
        {
            try
            {
                string role = "user";
                string createdAccountWith = "google";
                UserVM authenticatedUser = new UserVM();

                var payload = GoogleJsonWebSignature.ValidateAsync(tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;


                AuthenticateUserVM authenticateUser = new AuthenticateUserVM()
                {
                    FullName = payload.Name,
                    Email = payload.Email,
                    Password = "nopassword"
                };

                bool doesUserExist = authService.DoesUserExist(authenticateUser.Email);

                if (doesUserExist)
                {
                    authenticatedUser = authService.Login(authenticateUser);
                }

                authenticatedUser = authService.Register(authenticateUser, role, createdAccountWith);

                var claims = authService.SetCustomClaims(authenticatedUser.ID, authenticatedUser.Role);

                var userDetails = authService.GetToken(authenticatedUser, claims);

                return new JsonResult(userDetails);
            }
            catch (Exception e) { throw e; }
        }

    }
}
