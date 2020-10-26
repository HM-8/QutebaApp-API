using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class Category
    {
        public Category()
        {
            Incomes = new HashSet<Income>();
            Spendings = new HashSet<Spending>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public DateTime CategoryCreationTime { get; set; }

        public virtual Profile User { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Spending> Spendings { get; set; }
    }
}
