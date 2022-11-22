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
                        ID = income.Id,
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

        [HttpPatch]
        [Route("updateincome")]
        [Authorize(Roles = "user")]
        public IActionResult UpdateIncome(IncomeDashboardVM incomeDashboardVM)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var income = unitOfWork.IncomeRepository.FindBy(i => i.UserId == userId && i.Id == incomeDashboardVM.ID, "IncomeCategory");
            
            var category = unitOfWork.CategoryRepository.FindBy(c => c.CategoryName == incomeDashboardVM.IncomeCategoryName && c.CategoryType == "income");
            
            if (income != null && category != null && category.Id == income.IncomeCategory.Id)
            {
                unitOfWork.IncomeRepository.DetachEntry(income);
                unitOfWork.CategoryRepository.DetachEntry(category);

                Income updateIncome = new Income()
                {
                    Id = income.Id,
                    UserId = income.UserId,
                    IncomeCategoryId = category.Id,
                    IncomeAmount = incomeDashboardVM.IncomeAmount,
                    IncomeCreationTime = income.IncomeCreationTime
                };

                unitOfWork.IncomeRepository.Update(updateIncome);
                unitOfWork.Save();

                IncomeDashboardVM UpdateIncomeDashboard = new IncomeDashboardVM()
                { 
                    ID = updateIncome.Id,
                    IncomeCategoryName = category.CategoryName,
                    IncomeCreationTime = updateIncome.IncomeCreationTime,
                    IncomeAmount = updateIncome.IncomeAmount
                };

                return new JsonResult(UpdateIncomeDashboard);
            }

            return new JsonResult("Error");
        }

        [HttpDelete]
        [Route("deleteincome")]
        [Authorize(Roles = "user")]
        public IActionResult DeleteIncome([FromQuery] int incomeId)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var income = unitOfWork.IncomeRepository.FindBy(i => i.UserId == userId && i.Id == incomeId);

            if (income != null)
            {
                unitOfWork.IncomeRepository.Delete(incomeId);
                unitOfWork.Save();

                return new JsonResult("Deleted");
            }

            return new JsonResult("Error");
        }

        [HttpGet]
        [Route("listIncomeDaily")]
        [Authorize(Roles = "user")]
        public IActionResult ListIncomeDaily()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var incomes = unitOfWork.IncomeRepository.FindAllBy(i => i.UserId == userId);

            if (incomes.FirstOrDefault() != null)
            {
                List<ListDailyVM> listIncomeDailyVMs = new List<ListDailyVM>();

                foreach (var income in incomes)
                {
                    ListDailyVM listIncomeDailyVM = new ListDailyVM()
                    {
                        Date = income.IncomeCreationTime.ToString().Substring(0, 9),
                        Time = income.IncomeCreationTime.TimeOfDay.ToString(),
                        Amount = income.IncomeAmount
                    };

                    listIncomeDailyVMs.Add(listIncomeDailyVM);
                }

                return new JsonResult(listIncomeDailyVMs);
            }

            return new JsonResult("You currently have no income!");
        }
    }
}
