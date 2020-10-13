using FirebaseAdmin.Auth;
using QutebaApp_Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Implementations
{
    public class FirebaseService : IFirebaseService
    {
        public async Task<FirebaseToken> VerifyFirebaseToken(string token)
        {
            try
            {
                if (token != null)
                {
                    FirebaseToken verifiedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                    return verifiedToken;
                }

                return null;
            }
            catch (Exception e) { throw e; }
        }
    }
}
