using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System;

namespace QutebaApp_API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProfileController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("createprofile")]
        [Authorize(Roles = "user")]
        public IActionResult CreateProfile([FromBody] CreateProfileVM profileVM)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            Profile profile = new Profile()
            {
                UserId = userId,
                Username = profileVM.Username,
                PhotoUrl = profileVM.PhotoUrl,
                ProfileCreationTime = DateTime.Now
            };

            unitOfWork.ProfileRepository.Insert(profile);
            unitOfWork.Save();

            return new JsonResult($"Your profile has been created! Created at {DateTime.Now}");
        }

        [HttpPatch]
        [Route("updateprofile")]
        [Authorize(Roles = "user")]
        public IActionResult UpdateProfile([FromBody] CreateProfileVM profileVM)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            Profile profile = new Profile()
            {
                UserId = userId,
                Username = profileVM.Username,
                PhotoUrl = profileVM.PhotoUrl,
                ProfileCreationTime = DateTime.Now
            };

            unitOfWork.ProfileRepository.Update(profile);
            unitOfWork.Save();

            return new JsonResult($"Your profile has been updated! Updated at {DateTime.Now}");
        }
    }
}
