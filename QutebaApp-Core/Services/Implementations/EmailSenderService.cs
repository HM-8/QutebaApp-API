using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> SendForgotPasswordEmailAsync(DynamicTemplateDataVM dynamicTemplateData)
        {
            dynamic data = new JObject();
            data.name = dynamicTemplateData.Name;
            data.email = dynamicTemplateData.Email;
            data.code = dynamicTemplateData.Code;

            var client = new SendGridClient(configuration["SendGrid:ApiKey"]);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(configuration["SendGrid:SenderEmail"], configuration["SendGrid:Sender"]);
            sendGridMessage.AddTo(dynamicTemplateData.Email, dynamicTemplateData.Name);
            sendGridMessage.SetTemplateId(configuration["SendGrid:TemplateID"]);
            sendGridMessage.SetTemplateData(data);

            sendGridMessage.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(sendGridMessage).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return true;
            }

            return false;
        }
    }
}
