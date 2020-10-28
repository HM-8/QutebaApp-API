using Microsoft.Extensions.Configuration;
using QutebaApp_Core.Services.Interfaces;

namespace QutebaApp_Core.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
