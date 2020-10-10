using System;

namespace QutebaApp_Data.Models
{
    public partial class Account
    {
        public string Uid { get; set; }
        public int RoleId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public byte? EmailVerified { get; set; }
        public string CreatedAccountWith { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastSigninTime { get; set; }
        public DateTime? LastRefreshTime { get; set; }

        public virtual Role Role { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
