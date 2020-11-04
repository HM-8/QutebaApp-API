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
    [Route("api/income")]
    [ApiController]
    public class IncomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public IncomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("addincome")]
        [Authorize(Roles = "user")]
        public IActionResult AddIncome(CreateIncomeVM incomeVM)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var category = unitOfWork.CategoryRepository.GetById(incomeVM.IncomeCategoryId);

            if (category.CategoryType == "income")
            {
                Income income = new Income()
                {
                    UserId = userId,
                    IncomeCategoryId = incomeVM.IncomeCategoryId,
                    IncomeAmount = incomeVM.IncomeAmount,
                    IncomeCreationTime = DateTime.Now
                };

                unitOfWork.IncomeRepository.Insert(income);
                unitOfWork.Save();

                return new JsonResult($"Income has been added! Created at {DateTime.Now}");
            }

            return new JsonResult($"Error: income cannot be added because the category type is wrong!");
        }

        [HttpGet]
        [Route("getallincome")]
        [Authorize(Roles = "user")]
        public IActionResult GetAllIncome()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var incomes = unitOfWork.IncomeRepository.FindAllBy(i => i.UserId == userId, "IncomeCategory");

            if (incomes.FirstOrDefault() != null)
            {
                List<IncomeDashboardVM> incomesDashboardVM = new List<IncomeDashboardVM>();

                foreach (var income in incomes)
                {
                    IncomeDashboardVM incomeDashboardVM = new IncomeDashboardVM()
                    {
                        IncomeCategoryName = income.IncomeCategory.CategoryName,
                        IncomeCreationTime = income.IncomeCreationTime,
                        IncomeAmount = income.IncomeAmount
                    };

                    incomesDashboardVM.Add(incomeDashboardVM);
                }

                return new JsonResult(incomesDashboardVM);
            }

            return new JsonResult("You currently have no income!");

        }

        [HttpGet]
        [Route("gettotalbalance")]
        [Authorize(Roles = "user")]
        public IActionResult GetTotalBalance()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var incomes = unitOfWork.IncomeRepository.FindAllBy(i => i.UserId == userId);

            DateTime recentTransactionDate = DateTime.MinValue;

            double total = 0.0;

            if (incomes.FirstOrDefault() != null)
            {
                recentTransactionDate = incomes.OrderByDescending(i => i.IncomeCreationTime).FirstOrDefault().IncomeCreationTime;
                total = incomes.Select(i => i.IncomeAmount).Sum();
            }

            DashboardCardVM dashboardCardVM = new DashboardCardVM()
            {
                RecentTransactionDate = recentTransactionDate,
                Total = total
            };

            return new JsonResult(dashboardCardVM);
        }
    }
}
