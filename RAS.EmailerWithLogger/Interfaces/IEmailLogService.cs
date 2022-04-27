using System.Collections.Generic;
using System.Threading.Tasks;
using RAS.EmailerWithLogger.DomainModels;

namespace RAS.EmailerWithLogger.Interfaces
{
    public interface IEmailLogService
    {
        Task<IEnumerable<EmailLogDomainModel>> GetAsync(int? year, int? month,
            int? day);

        Task<IEnumerable<EmailLogDomainModel>> GetByTemplateIdAsync(string templateId, int? year, int? month,
            int? day);
    }
}