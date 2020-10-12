using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IAuthService
    {

        Task SetCustomClaims(string uid, string role);
    }
}
