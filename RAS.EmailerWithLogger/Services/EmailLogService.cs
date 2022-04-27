using System.Collections.Generic;
using System.Threading.Tasks;
using E.S.Data.Query.Context.Extensions;
using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.DataAccess.Interfaces;
using RAS.EmailerWithLogger.DomainModels;
using RAS.EmailerWithLogger.Interfaces;

namespace RAS.EmailerWithLogger.Services
{
    public class EmailLogService : IEmailLogService
    {
        private readonly IDataAccessQuery _dataAccessQuery;
        private readonly IRepositoryService<EmailLogDomainModel> _emailLogRepositoryService;

        public EmailLogService(IRepositoryService<EmailLogDomainModel> emailLogRepositoryService,
            IDataAccessQuery dataAccessQuery)
        {
            _emailLogRepositoryService = emailLogRepositoryService;
            _dataAccessQuery = dataAccessQuery;
        }

        public async Task<IEnumerable<EmailLogDomainModel>> GetAsync(int? year, int? month,
            int? day)
        {
            var emailLogSelectBuilder = _dataAccessQuery.SelectQuery<EmailLogDomainModel>();

            if (year.HasValue) emailLogSelectBuilder = emailLogSelectBuilder.Where("Year", year.Value);

            if (month.HasValue) emailLogSelectBuilder = emailLogSelectBuilder.Where("Month", month.Value);

            if (day.HasValue) emailLogSelectBuilder = emailLogSelectBuilder.Where("Day", day.Value);

            var emailLogs = await emailLogSelectBuilder
                .ListAsync<EmailLogDomainModel>();

            return emailLogs;
        }

        public async Task<IEnumerable<EmailLogDomainModel>> GetByTemplateIdAsync(string templateId, int? year,
            int? month,
            int? day)
        {
            var emailLogSelectBuilder = _dataAccessQuery.SelectQuery<EmailLogDomainModel>()
                .Where("TemplateId", templateId);

            if (year.HasValue) emailLogSelectBuilder =emailLogSelectBuilder.Where("Year", year.Value);

            if (month.HasValue) emailLogSelectBuilder =emailLogSelectBuilder.Where("Month", month.Value);

            if (day.HasValue) emailLogSelectBuilder =emailLogSelectBuilder.Where("Day", day.Value);

            var emailLogs = await emailLogSelectBuilder
                .ListAsync<EmailLogDomainModel>();

            return emailLogs;
        }
    }
}