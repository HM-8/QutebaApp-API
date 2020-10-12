using System.Collections.Generic;

namespace QutebaApp_Data.ViewModels
{
    public class AuthenticatedAdminVM
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IReadOnlyDictionary<string, object> Claims { get; set; }
    }
}
