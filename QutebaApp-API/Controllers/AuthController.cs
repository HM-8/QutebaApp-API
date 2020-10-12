using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("register/user")]
        public async Task<ActionResult<AuthenticatedUserVM>> RegisterUser([FromForm] string token)
        {
            try
            {
                string role = "user";
                GeneralUserVM generalUserVM = await authService.Register(token, role);

                AuthenticatedUserVM authenticatedUserVM = new AuthenticatedUserVM()
                {
                    UID = generalUserVM.UID,
                    Name = generalUserVM.Name,
                    Email = generalUserVM.Email,
                    Profile = new Profile()
                    {
                        UserUid = generalUserVM.UID,
                        PhotoUrl = null,
                        Salary = 10000,
                        SalaryCreationTime = null
                    },
                    Claims = generalUserVM.Claims
                };

                return new JsonResult(authenticatedUserVM);
            }
            catch (Exception e) { throw e; }
        }

        [HttpPost]
        [Route("register/admin")]
        public async Task<ActionResult<AuthenticatedAdminVM>> RegisterAdmin([FromForm] string token)
        {
            try
            {
                string role = "admin";
                GeneralUserVM generalUserVM = await authService.Register(token, role);

                AuthenticatedAdminVM authenticatedAdminVM = new AuthenticatedAdminVM()
                {
                    UID = generalUserVM.UID,
                    Name = generalUserVM.Name,
                    Email = generalUserVM.Email,
                    Claims = generalUserVM.Claims
                };

                return new JsonResult(authenticatedAdminVM);
            }
            catch (Exception e) { throw e; }
        }

        [HttpPost]
        [Route("login/user")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<AuthenticatedUserVM>> LogInUser([FromForm] string token)
        {
            try
            {
                GeneralUserVM generalUserVM = await authService.Login(token);

                AuthenticatedUserVM authenticatedUserVM = new AuthenticatedUserVM()
                {
                    UID = generalUserVM.UID,
                    Name = generalUserVM.Name,
                    Email = generalUserVM.Email,
                    Profile = new Profile()
                    {
                        UserUid = generalUserVM.UID,
                        PhotoUrl = null,
                        Salary = 10000,
                        SalaryCreationTime = null
                    },
                    Claims = generalUserVM.Claims
                };

                return new JsonResult(authenticatedUserVM);
            }
            catch (Exception e) { throw e; }
        }
    }
}
