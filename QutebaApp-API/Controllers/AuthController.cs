using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Data.Models;
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
        public async Task<User> CreateAccountWithEmailAndPassword([FromBody] User user)
        {
            try
            {
                UserRecordArgs userDetails = new UserRecordArgs()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Password = user.Password,
                };

                UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userDetails);
                IUserInfo[] userInfo = userRecord.ProviderData;

                User createdUser = new User()
                {
                    DisplayName = userRecord.DisplayName,
                    Email = userRecord.Email
                };

                return createdUser;
            }
            catch (Exception e) { throw e; }

        }
    }
}
