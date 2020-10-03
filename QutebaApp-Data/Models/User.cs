using FirebaseAdmin.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace QutebaApp_Data.Models
{
    public class User
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string PhotoUrl { get; set; }
        public string CreatedAccountWith { get; set; }
        public DateTime? CreationTimestamp { get; set; }
        public DateTime? LastSignInTimestamp { get; set; }
        public DateTime? LastRefreshTimestamp { get; set; }
    }
}
