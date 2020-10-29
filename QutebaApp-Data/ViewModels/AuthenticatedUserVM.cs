using System;

namespace QutebaApp_Data.ViewModels
{
    public class AuthenticatedUserVM
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
