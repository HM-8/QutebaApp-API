using System;
using System.Collections.Generic;
using System.Text;

namespace QutebaApp_Data.ViewModels
{
    public class CreateSpendingVM
    {
        public int SpendingCategoryId { get; set; }
        public double SpendingAmount { get; set; }
        public string Reason { get; set; }
    }
}
