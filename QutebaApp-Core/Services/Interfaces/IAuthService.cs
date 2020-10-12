using QutebaApp_Data.ViewModels;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<GeneralUserVM> Register(string token, string role);
        Task<GeneralUserVM> Login(string token);
        Task SetCustomClaims(string uid, string role);
    }
}
