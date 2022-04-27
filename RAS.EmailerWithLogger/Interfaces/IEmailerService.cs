using System.Threading.Tasks;
using RAS.Emailer.Interfaces;

namespace RAS.EmailerWithLogger.Services
{
    public interface IEmailerService
    {
        Task SendEmailAsync(string templateId, IEmailItem emailItem, params string[] toEmails);
    }
}