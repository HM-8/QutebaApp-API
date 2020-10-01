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
        [Route("create")]
        public async Task<IUserInfo[]> CreateUser([FromBody] User user)
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

        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<UserRecord>>> GetAllUsers()
        {
            List<UserRecord> users = new List<UserRecord>();

            var responses = FirebaseAuth.DefaultInstance.ListUsersAsync(null).AsRawResponses().GetEnumerator();

            Console.WriteLine($"RESPONSES >>> {responses}");

            while (await responses.MoveNext())
            {
                ExportedUserRecords result = responses.Current;

                foreach (ExportedUserRecord record in result.Users)
                {
                    UserRecord user = record;
                    users.Add(user);
                }
            }
            return Ok(users);

        }

    }
}
