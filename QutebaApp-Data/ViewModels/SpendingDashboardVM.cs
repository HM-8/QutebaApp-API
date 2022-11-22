using System;

namespace QutebaApp_Data.ViewModels
{
    public class SpendingDashboardVM
    {
        public int ID { get; set; }
        public string SpendingCategoryName { get; set; }
        public DateTime SpendingCreationTime { get; set; }
        public double SpendingAmount { get; set; }
    }
}
