using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class Spending
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public double Amount { get; set; }
        public string Reason { get; set; }
        public DateTime SpendingCreationTime { get; set; }

        public virtual Category Category { get; set; }
        public virtual Profile User { get; set; }
    }
}
