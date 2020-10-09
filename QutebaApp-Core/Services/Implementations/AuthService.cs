using FirebaseAdmin.Auth;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.ViewModels;
using System;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public bool CreateAccountWithEmailAndPassword(AuthenticateUserVM authenticateUserVM)
        {
            try
            {
                bool isCompleted = false;
                string uid = null;

                UserRecordArgs userDetails = new UserRecordArgs()
                {
                    DisplayName = authenticateUserVM.DisplayName,
                    Email = authenticateUserVM.Email,
                    Password = authenticateUserVM.Password,
                };

                if( userDetails.DisplayName != null && userDetails.Email != null && userDetails.Password != null)
                {
                    uid = FirebaseAuth.DefaultInstance.CreateUserAsync(userDetails).Result.Uid;
                }


                if (uid != null)
                {
                    isCompleted = true;

                    return isCompleted;
                }

                return isCompleted;
            }
            catch (Exception e) { throw e; }
        }
    }
}
