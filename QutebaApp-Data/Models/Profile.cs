using System;
using System.Collections.Generic;

namespace QutebaApp_Data.Models
{
    public partial class Profile
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime ProfileCreationTime { get; set; }

        public virtual User User { get; set; }
    }
}
