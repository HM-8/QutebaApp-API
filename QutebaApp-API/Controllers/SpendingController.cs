using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QutebaApp_API.Controllers
{
    [Route("api/spending")]
    [ApiController]
    public class SpendingController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public SpendingController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("addspending")]
        [Authorize(Roles = "user")]
        public IActionResult AddIncome(CreateSpendingVM spendingVM)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var category = unitOfWork.CategoryRepository.GetById(spendingVM.SpendingCategoryId);

            if (category.CategoryType == "spending")
            {
                Spending spending = new Spending()
                {
                    UserId = userId,
                    SpendingCategoryId = spendingVM.SpendingCategoryId,
                    SpendingAmount = spendingVM.SpendingAmount,
                    Reason = spendingVM.Reason,
                    SpendingCreationTime = DateTime.Now
                };

                unitOfWork.SpendingRepository.Insert(spending);
                unitOfWork.Save();

                return new JsonResult($"Spending has been added! Created at {DateTime.Now}");
            }

            return new JsonResult($"Error: spending cannot be added because the category type is wrong!");
        }

        [HttpGet]
        [Route("getallspending")]
        [Authorize(Roles = "user")]
        public IActionResult GetAllSpending()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var spendings = unitOfWork.SpendingRepository.FindAllBy(i => i.UserId == userId, "SpendingCategory");

            if (spendings.FirstOrDefault() != null)
            {
                List<SpendingDashboardVM> spendingsDashboardVM = new List<SpendingDashboardVM>();

                foreach (var spending in spendings)
                {
                    SpendingDashboardVM spendingDashboardVM = new SpendingDashboardVM()
                    {
                        SpendingCategoryName = spending.SpendingCategory.CategoryName,
                        SpendingCreationTime = spending.SpendingCreationTime,
                        SpendingAmount = spending.SpendingAmount
                    };

                    spendingsDashboardVM.Add(spendingDashboardVM);
                }

                return new JsonResult(spendingsDashboardVM);
            }

            return new JsonResult("You currently have no spendings!");
        }
    }
}
