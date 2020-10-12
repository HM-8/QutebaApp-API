using FirebaseAdmin.Auth;
using QutebaApp_Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task SetCustomClaims(string uid, string role)
        {
            try
            {
                UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

                Dictionary<string, object> userClaims = (Dictionary<string, object>)userRecord.CustomClaims;

                bool isUser = userClaims.ContainsValue(role);

                if (!isUser)
                {
                    Dictionary<string, object> keyClaims = new Dictionary<string, object>();
                    keyClaims.Add("role", role);

                    IReadOnlyDictionary<string, object> claims = keyClaims;

                    FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims).Wait();

                    Console.WriteLine($"USER CLAIM >>>> {userRecord.CustomClaims}");

                }


            }
            catch (Exception e) { throw e; }
        }
    }
}
