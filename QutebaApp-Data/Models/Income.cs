using System;

namespace QutebaApp_Data.Models
{
    public partial class Income
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IncomeCategoryId { get; set; }
        public double IncomeAmount { get; set; }
        public DateTime IncomeCreationTime { get; set; }

        public virtual Category IncomeCategory { get; set; }
        public virtual User User { get; set; }
    }
}
