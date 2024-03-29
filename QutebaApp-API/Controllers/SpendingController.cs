﻿using Microsoft.AspNetCore.Authorization;
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
        public IActionResult AddSpending(CreateSpendingVM spendingVM)
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
                        ID = spending.Id,
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

        [HttpGet]
        [Route("gettotalbalance")]
        [Authorize(Roles = "user")]
        public IActionResult GetTotalBalance()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var spendings = unitOfWork.SpendingRepository.FindAllBy(i => i.UserId == userId);

            DateTime recentTransactionDate = DateTime.MinValue;

            double total = 0.0;

            if (spendings.FirstOrDefault() != null)
            {
                recentTransactionDate = spendings.OrderByDescending(s => s.SpendingCreationTime).FirstOrDefault().SpendingCreationTime;
                total = spendings.Select(s => s.SpendingAmount).Sum();
            }

            DashboardCardVM dashboardCardVM = new DashboardCardVM()
            {
                RecentTransactionDate = recentTransactionDate,
                Total = total
            };

            return new JsonResult(dashboardCardVM);
        }

        [HttpPatch]
        [Route("updatespending")]
        [Authorize(Roles = "user")]
        public IActionResult UpdateSpending(SpendingDashboardVM spendingDashboardVM)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var spending = unitOfWork.SpendingRepository.FindBy(i => i.UserId == userId && i.Id == spendingDashboardVM.ID, "SpendingCategory");
            
            var category = unitOfWork.CategoryRepository.FindBy(c => c.UserId == userId && c.CategoryName == spendingDashboardVM.SpendingCategoryName && c.CategoryType == "spending");
            
            if (spending != null && category != null && category.Id == spending.SpendingCategoryId)
            {
                unitOfWork.SpendingRepository.DetachEntry(spending);
                unitOfWork.CategoryRepository.DetachEntry(category);

                Spending updateSpending = new Spending()
                {
                    Id = spending.Id,
                    UserId = spending.UserId,
                    SpendingCategoryId = category.Id,
                    SpendingAmount = spendingDashboardVM.SpendingAmount,
                    SpendingCreationTime = spending.SpendingCreationTime,
                };

                unitOfWork.SpendingRepository.Update(updateSpending);
                unitOfWork.Save();

                SpendingDashboardVM updatespendingDashboard = new SpendingDashboardVM()
                {
                    ID = updateSpending.Id,
                    SpendingCategoryName = category.CategoryName,
                    SpendingCreationTime = updateSpending.SpendingCreationTime,
                    SpendingAmount = updateSpending.SpendingAmount
                };

                return new JsonResult(updatespendingDashboard);
            }

            return new JsonResult("Error");
        }

        [HttpDelete]
        [Route("deletespending")]
        [Authorize(Roles = "user")]
        public IActionResult DeleteSpending([FromQuery] int spendingId)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var spending = unitOfWork.SpendingRepository.FindBy(s => s.UserId == userId && s.Id == spendingId);

            if (spending != null)
            {
                unitOfWork.SpendingRepository.Delete(spendingId);
                unitOfWork.Save();

                return new JsonResult("Deleted");
            }

            return new JsonResult("Error");
        }

        [HttpGet]
        [Route("listSpendingDaily")]
        [Authorize(Roles = "user")]
        public IActionResult ListSpendingDaily()
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirst("userId").Value);

            var spendings = unitOfWork.SpendingRepository.FindAllBy(i => i.UserId == userId);

            if (spendings.FirstOrDefault() != null)
            {
                List<ListDailyVM> listSpendingDailyVMs = new List<ListDailyVM>();

                foreach (var spending in spendings)
                {
                    ListDailyVM listSpendingDailyVM = new ListDailyVM()
                    {
                        Date = spending.SpendingCreationTime.ToString().Substring(0,9),
                        Time = spending.SpendingCreationTime.TimeOfDay.ToString(),
                        Amount = spending.SpendingAmount
                    };

                    listSpendingDailyVMs.Add(listSpendingDailyVM);
                }

                return new JsonResult(listSpendingDailyVMs);
            }

            return new JsonResult("You currently have no spending!");
        }
    }
}
