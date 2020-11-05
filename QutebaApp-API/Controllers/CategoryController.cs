using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.OtherModels;
using QutebaApp_Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet]
        [Route("getallcategories")]
        [Authorize(Roles = "user")]
        public IActionResult GetAllCategories()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var categories = unitOfWork.CategoryRepository.FindAllBy(c => c.UserId == userId);

            if (categories.FirstOrDefault() != null)
            {
                List<CategoryVM> categoryVMs = new List<CategoryVM>();

                foreach (var category in categories)
                {
                    CategoryVM categoryVM = new CategoryVM()
                    { 
                        Id = category.Id,
                        CategoryName = category.CategoryName,
                        CategoryType = category.CategoryType,
                        CategoryCreationTime = category.CategoryCreationTime
                    };

                    categoryVMs.Add(categoryVM);
                }

                return new JsonResult(categoryVMs);
            }

            return new JsonResult("You currently have no categories!");
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

            var user = unitOfWork.UserRepository.GetById(Convert.ToInt32(HttpContext.User.FindFirst("userId").Value));

            if (user != null)
            {
                Category category = new Category()
                {
                    UserId = user.Id,
                    CategoryName = categoryVM.CategoryName,
                    CategoryType = categoryType,
                    CategoryCreationTime = DateTime.Now
                };

                unitOfWork.CategoryRepository.Insert(category);
                unitOfWork.Save();

                return new JsonResult($"Category {categoryVM.CategoryName} has been added! Created at {DateTime.Now}");
            }

            return new JsonResult($"Error: Can't seem to create category!");
        }

        [HttpGet]
        [Route("categorieswithcount")]
        [Authorize(Roles = "user")]
        public IActionResult CategoriesWithCount([FromQuery] int pageId)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);
            string categoryType = null;

            if (pageId == (int)PageTypes.IncomeCategory)
            {
                categoryType = "income";

                var categories = unitOfWork.CategoryRepository.FindAllBy(c => c.UserId == userId && c.CategoryType == categoryType, "Incomes");

                List<CategoryWithCountVM> categoriesWithCount = new List<CategoryWithCountVM>();

                foreach (var category in categories)
                {
                    CategoryWithCountVM categoryWithCount = new CategoryWithCountVM()
                    {
                        CategoryName = category.CategoryName,
                        Count = category.Incomes.Count()
                    };

                    categoriesWithCount.Add(categoryWithCount);
                }

                return new JsonResult(categoriesWithCount);
            }

            if (pageId == (int)PageTypes.SpendingCategory)
            {
                categoryType = "spending";

                var categories = unitOfWork.CategoryRepository.FindAllBy(c => c.UserId == userId && c.CategoryType == categoryType, "Spendings");

                List<CategoryWithCountVM> categoriesWithCount = new List<CategoryWithCountVM>();

                foreach (var category in categories)
                {
                    CategoryWithCountVM categoryWithCount = new CategoryWithCountVM()
                    {
                        CategoryName = category.CategoryName,
                        Count = category.Spendings.Count()
                    };

                    categoriesWithCount.Add(categoryWithCount);
                }

                return new JsonResult(categoriesWithCount);
            }

            return new JsonResult("Error: Wrong pageId!");
        }
    }
}
