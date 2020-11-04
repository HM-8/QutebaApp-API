using System;

namespace QutebaApp_Data.ViewModels
{
    public class IncomeDashboardVM
    {
        public int ID { get; set; }
        public string IncomeCategoryName { get; set; }
        public DateTime IncomeCreationTime { get; set; }
        public double IncomeAmount { get; set; }
    }
}
