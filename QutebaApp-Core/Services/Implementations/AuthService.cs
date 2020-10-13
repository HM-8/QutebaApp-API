using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private IFirebaseService firebaseService;

        public AuthService(IFirebaseService firebaseService)
        {
            this.firebaseService = firebaseService;
        }


        public async Task<GeneralUserVM> Register(string token, string role)
        {
            try
            {
                // add role to claim and save user in database and return user

                var verifiedToken = await firebaseService.VerifyFirebaseToken(token);
                string uid = verifiedToken.Uid;

                await SetCustomClaims(uid, role);

                var firebaseUser = await firebaseService.GetFirebaseUserById(uid);

                GeneralUserVM generalUserVM = new GeneralUserVM()
                {
                    UID = firebaseUser.Uid,
                    Name = firebaseUser.DisplayName,
                    Email = firebaseUser.Email,
                    Claims = firebaseUser.CustomClaims
                };

                return generalUserVM;

            }
            catch (Exception e) { throw e; }
        }

        public async Task<GeneralUserVM> Login(string token)
        {
            try
            {
                var verifiedToken = await firebaseService.VerifyFirebaseToken(token);
                string uid = verifiedToken.Uid;
                var firebaseUser = await firebaseService.GetFirebaseUserById(uid);

                GeneralUserVM generalUserVM = new GeneralUserVM()
                {
                    UID = firebaseUser.Uid,
                    Name = firebaseUser.DisplayName,
                    Email = firebaseUser.Email,
                    Claims = firebaseUser.CustomClaims
                };

                return generalUserVM;
            }
            catch (Exception e) { throw e; }
        }

        public async Task SetCustomClaims(string uid, string role)
        {
            try
            {
                var firebaseUser = await firebaseService.GetFirebaseUserById(uid);

                Dictionary<string, object> userClaims = (Dictionary<string, object>)firebaseUser.CustomClaims;

                bool isUser = userClaims.ContainsValue(role);

                if (!isUser)
                {
                    Dictionary<string, object> keyClaims = new Dictionary<string, object>();
                    keyClaims.Add("role", role);

                    IReadOnlyDictionary<string, object> claims = keyClaims;

                    await firebaseService.SetCustomFirebaseUserClaims(uid, claims);

                    Console.WriteLine($"USER CLAIM >>>> {firebaseUser.CustomClaims}");

                }


            }
            catch (Exception e) { throw e; }
        }
    }
}
