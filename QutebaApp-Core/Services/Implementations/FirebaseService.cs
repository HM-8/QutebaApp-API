using FirebaseAdmin.Auth;
using QutebaApp_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task SetCustomFirebaseUserClaims(string uid, IReadOnlyDictionary<string, object> claims)
        {
            try
            {
                return FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);
            }
            catch (Exception e) { throw e; }
        }

        public async Task<UserRecord> GetFirebaseUserById(string uid)
        {
            try
            {
                if (uid != null)
                {
                    UserRecord firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
                    return firebaseUser;
                }

                return null;
            }
            catch (Exception e) { throw e; }
        }
    }
}
