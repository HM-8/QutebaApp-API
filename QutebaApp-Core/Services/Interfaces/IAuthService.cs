using QutebaApp_Data.ViewModels;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IAuthService
    {
        bool CreateAccountWithEmailAndPassword(AuthenticateUserVM authenticateUserVM);
    }
}
