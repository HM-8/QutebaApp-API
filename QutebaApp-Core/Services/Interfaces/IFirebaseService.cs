using FirebaseAdmin.Auth;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task<FirebaseToken> VerifyFirebaseToken(string token);
        Task<UserRecord> GetFirebaseUserById(string uid);
    }
}
