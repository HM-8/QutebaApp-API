using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace QutebaApp_Data.ViewModels
{
    public class AuthenticatedUserVM
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
