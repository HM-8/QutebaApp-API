using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> CreateAccountWithEmailAndPassword(AuthenticateUserVM authenticateUserVM);
    }
}
