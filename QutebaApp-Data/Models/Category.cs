using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class Category
    {
        public Category()
        {
            Spendings = new HashSet<Spending>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CategoryCreationTime { get; set; }

        public virtual ICollection<Spending> Spendings { get; set; }
    }
}
