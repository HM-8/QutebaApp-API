using QutebaApp_Data.ViewModels;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IAuthService
    {
        bool CreateAccountWithEmailAndPassword(AuthenticateUserVM authenticateUserVM);
    }
}
