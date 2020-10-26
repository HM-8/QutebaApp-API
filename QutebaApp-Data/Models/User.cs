using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public int RoleId { get; set; }
        public string CreatedAccountWith { get; set; }
        public DateTime UserCreationTime { get; set; }

        public virtual Role Role { get; set; }
        public virtual Profile Profiles { get; set; }
    }
}
