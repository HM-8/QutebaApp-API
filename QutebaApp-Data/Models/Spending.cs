using System;

namespace QutebaApp_Data.Models
{
    public partial class Spending
    {
        public int Id { get; set; }
        public string UserUid { get; set; }
        public int CategoryId { get; set; }
        public double Amount { get; set; }
        public string Reason { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual Category Category { get; set; }
        public virtual Profile User { get; set; }
    }
}
