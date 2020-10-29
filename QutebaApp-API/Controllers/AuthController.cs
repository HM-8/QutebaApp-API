using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
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
        private readonly IEmailSenderService emailSenderService;
        private readonly IUnitOfWork unitOfWork;
        public AuthController(IAuthService authService, IUnitOfWork unitOfWork, IEmailSenderService emailSenderService)
        {
            this.authService = authService;
            this.unitOfWork = unitOfWork;
            this.emailSenderService = emailSenderService;
        }


        [HttpPost]
        [Route("signup-Email")]
        [AllowAnonymous]
        public IActionResult SignUpEmail([FromBody] SignUpUserVM authenticateUser, [FromQuery] int pageId)
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
        [AllowAnonymous]
        public IActionResult SignInEmail([FromBody] SignInUserVM authenticateUser)
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
        [AllowAnonymous]
        public IActionResult Google([FromQuery] string tokenId)
        {
            try
            {
                string role = "user";
                string createdAccountWith = "google";
                UserVM authenticatedUser = new UserVM();

                var payload = GoogleJsonWebSignature.ValidateAsync(tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;

                bool doesUserExist = authService.DoesUserExist(payload.Email);

                if (doesUserExist)
                {
                    SignInUserVM signInUser = new SignInUserVM()
                    {
                        Email = payload.Email,
                        Password = "nopassword"
                    };

                    authenticatedUser = authService.Login(signInUser);
                }
                else
                {
                    SignUpUserVM signUpUser = new SignUpUserVM()
                    {
                        FullName = payload.Name,
                        Email = payload.Email,
                        Password = "nopassword"
                    };

                    authenticatedUser = authService.Register(signUpUser, role, createdAccountWith);
                }

                var claims = authService.SetCustomClaims(authenticatedUser.ID, authenticatedUser.Role);

                var userDetails = authService.GetToken(authenticatedUser, claims);

                return new JsonResult(userDetails);
            }
            catch (Exception e) { throw e; }
        }

        [HttpPost]
        [Route("password/sendpasswordresetcode")]
        public IActionResult SendPasswordResetCode([FromBody] ForgotPasswordVM forgotPasswordVM)
        {
            var user = unitOfWork.UserRepository.FindBy(u => u.Email == forgotPasswordVM.Email);

            if (user != null)
            {
                string username = user.Fullname.Split(" ")[0];
                var code = authService.GenerateCode();

                DynamicTemplateDataVM templateDataVM = new DynamicTemplateDataVM()
                {
                    Name = username,
                    Email = user.Email,
                    Code = code.ToString()
                };

                var response = emailSenderService.SendForgotPasswordEmailAsync(templateDataVM);

                if (response.Result == true)
                {
                    unitOfWork.CodeRepository.Insert(new Code() { UserId = user.Id, CodeDigit = code, TimeCreated = DateTime.Now });
                    unitOfWork.Save();

                    return new JsonResult($"Code has been sent to {user.Email}.");
                }

                return new JsonResult($"Error: Code has not been sent to {user.Email}.");
            }

            return new JsonResult($"Error: User does not exist!");

        }

        [HttpPatch]
        [Route("password/resetforgottenpassword")]
        public IActionResult ResetForgottenPassword([FromBody] ResetPasswordCodeVM resetPasswordCodeVM)
        {
            var user = unitOfWork.UserRepository.FindBy(u => u.Email == resetPasswordCodeVM.Email);
            unitOfWork.UserRepository.DetachEntry(user);

            if (user != null)
            {
                var code = unitOfWork.CodeRepository.FindBy(u => u.UserId == user.Id && u.CodeDigit == Convert.ToInt32(resetPasswordCodeVM.Code));
                unitOfWork.CodeRepository.DetachEntry(code);

                if (code != null)
                {
                    resetPasswordCodeVM.NewPasssword = authService.Encrypt(resetPasswordCodeVM.NewPasssword);

                    User updateUser = new User()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = resetPasswordCodeVM.NewPasssword,
                        Fullname = user.Fullname,
                        RoleId = user.RoleId,
                        CreatedAccountWith = user.CreatedAccountWith,
                        UserCreationTime = user.UserCreationTime
                    };

                    unitOfWork.UserRepository.Update(updateUser);

                    unitOfWork.CodeRepository.Delete(code.ID);

                    unitOfWork.Save();

                    return new JsonResult($"You password is now updated!!");
                }

                return new JsonResult($"Error: Incorrect code!");
            }

            return new JsonResult($"Error: User does not exist!");
        }

    }
}
