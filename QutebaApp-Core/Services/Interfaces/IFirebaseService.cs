using FirebaseAdmin.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task<FirebaseToken> VerifyFirebaseToken(string token);
        Task SetCustomFirebaseUserClaims(string uid, IReadOnlyDictionary<string, object> claims);
        Task<UserRecord> GetFirebaseUserById(string uid);
    }
}
