using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Categories = new HashSet<Category>();
            Incomes = new HashSet<Income>();
            Spendings = new HashSet<Spending>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime ProfileCreationTime { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Spending> Spendings { get; set; }
    }
}
