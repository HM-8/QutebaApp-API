using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.OtherModels;
using QutebaApp_Data.ViewModels;
using System;

namespace QutebaApp_API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("addcategory")]
        [Authorize(Roles = "user")]
        public IActionResult AddCategory([FromBody] CreateCategoryVM categoryVM, [FromQuery] int pageId)
        {
            string categoryType = null;

            if (pageId == (int)PageTypes.IncomeCategory)
            {
                categoryType = "income";
            }
            if (pageId == (int)PageTypes.SpendingCategory)
            {
                categoryType = "spending";
            }

            var user = unitOfWork.ProfileRepository.GetById(Convert.ToInt32(HttpContext.User.FindFirst("userId").Value));

            if (user != null)
            {
                Category category = new Category()
                {
                    UserId = user.UserId,
                    CategoryName = categoryVM.CategoryName,
                    CategoryType = categoryType,
                    CategoryCreationTime = DateTime.Now
                };

                unitOfWork.CategoryRepository.Insert(category);
                unitOfWork.Save();

                return new JsonResult($"Category {categoryVM.CategoryName} has been added! Created at {DateTime.Now}");
            }

            return new JsonResult($"Error: You don't have a profile.");
        }
    }
}
