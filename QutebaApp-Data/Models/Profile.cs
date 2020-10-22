using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Spendings = new HashSet<Spending>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public double? Income { get; set; }
        public DateTime IncomeCreationTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Spending> Spendings { get; set; }
    }
}
