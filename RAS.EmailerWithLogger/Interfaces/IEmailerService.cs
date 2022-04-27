using System.Threading.Tasks;
using RAS.Emailer.Interfaces;
using RAS.EmailerWithLogger.Models;
using SendGrid;

namespace RAS.EmailerWithLogger.Interfaces
{
    public interface IEmailerService
    {
        Task<Response> SendEmailAsync(EmailItemBase emailItemBase)
        {
            return SendEmailAsync(emailItemBase.TemplateId, emailItemBase,
                emailItemBase.ToEmails.ToArray());
        }

        Task<Response> SendEmailAsync(EmailItemBase emailItemBase, params string[] toEmails)
        {
            return SendEmailAsync(emailItemBase.TemplateId, emailItemBase,
                toEmails);
        }

        Task<Response> SendEmailAsync(string templateId, IEmailItem emailItem, params string[] toEmails);
        
        void SetEnable(bool enable);
    }
}