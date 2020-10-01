using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QutebaApp_Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QutebaApp_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {

        [HttpPost]
        [Route("CreateAccountWithEmailAndPassword")]
        public async Task<IUserInfo[]> CreateUserWithEmailAndPassword([FromBody] User user)
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

                return userInfo;
            }
            catch (Exception e) { throw e; }

        }
    }
}
