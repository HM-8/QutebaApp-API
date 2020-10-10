using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
