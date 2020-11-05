using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class User
    {
        public User()
        {
            Categories = new HashSet<Category>();
            Incomes = new HashSet<Income>();
            Spendings = new HashSet<Spending>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int RoleId { get; set; }
        public string CreatedAccountWith { get; set; }
        public DateTime UserCreationTime { get; set; }

        public virtual Role Role { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Code Code { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Spending> Spendings { get; set; }
    }
}
