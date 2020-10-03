using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Data.Models;
using QutebaApp_Data.ViewModels;
using System;
using System.Threading.Tasks;

namespace QutebaApp_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {

        [HttpPost]
        [Route("CreateAccountWithEmailAndPassword")]
        public async Task<User> CreateAccountWithEmailAndPassword([FromBody] AuthenticateUserVM authenticateUserVM)
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
