using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;
using System;
using System.Linq;

namespace QutebaApp_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("gettotalbalance")]
        [Authorize(Roles = "user")]
        public IActionResult GetTotalBalance()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var incomes = unitOfWork.IncomeRepository.FindAllBy(i => i.UserId == userId);

            if (incomes != null)
            {
                double total = 0;
                foreach (var item in incomes)
                {
                    total += item.IncomeAmount;
                }

                return new JsonResult(total);
            }

            return new JsonResult("You have zero income");
        }
    }
}
