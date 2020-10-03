using FirebaseAdmin.Auth;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task<User> CreateAccountWithEmailAndPassword(AuthenticateUserVM authenticateUserVM)
        {
            try
            {
                UserRecordArgs userDetails = new UserRecordArgs()
                {
                    DisplayName = authenticateUserVM.DisplayName,
                    Email = authenticateUserVM.Email,
                    Password = authenticateUserVM.Password,
                };

                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userDetails);

                User createdUser = new User()
                {
                    ID = userRecord.Uid,
                    DisplayName = userRecord.DisplayName,
                    Email = userRecord.Email,
                    EmailVerified = userRecord.EmailVerified,
                    PhotoUrl = userRecord.PhotoUrl,
                    CreatedAccountWith = userRecord.ProviderData[0].ProviderId,
                    CreationTimestamp = userRecord.UserMetaData.CreationTimestamp,
                    LastSignInTimestamp = userRecord.UserMetaData.LastSignInTimestamp,
                    LastRefreshTimestamp = userRecord.UserMetaData.LastRefreshTimestamp
                };

                return createdUser;
            }
            catch (Exception e) { throw e; }
        }
    }
}
