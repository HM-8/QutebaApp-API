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

        public string UserUid { get; set; }
        public string PhotoUrl { get; set; }
        public double? Salary { get; set; }
        public DateTime? SalaryCreationTime { get; set; }

        public virtual Account User { get; set; }
        public virtual ICollection<Spending> Spendings { get; set; }
    }
}
