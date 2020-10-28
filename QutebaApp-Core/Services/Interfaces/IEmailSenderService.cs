using QutebaApp_Data.ViewModels;
using System.Threading.Tasks;

namespace QutebaApp_Core.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task<bool> SendForgotPasswordEmailAsync(DynamicTemplateDataVM dynamicTemplateData);
    }
}
