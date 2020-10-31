using Microsoft.AspNetCore.Mvc;
using QutebaApp_Core.Services.Interfaces;

namespace QutebaApp_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
