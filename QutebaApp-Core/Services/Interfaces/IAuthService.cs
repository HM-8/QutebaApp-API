using QutebaApp_Data.ViewModels;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<GeneralUserVM> Register(string token, string role);
        Task SetCustomClaims(string uid, string role);
    }
}
