using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;

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
    }
}
