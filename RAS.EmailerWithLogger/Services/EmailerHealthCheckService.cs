using System;
using System.Linq;
using System.Threading.Tasks;
using RAS.EmailerWithLogger.Enums;
using RAS.EmailerWithLogger.Interfaces;

namespace RAS.EmailerWithLogger.Services
{
    public class EmailerHealthCheckService : IEmailerHealthCheckService
    {
        private readonly IEmailLogService _emailLogService;

        public EmailerHealthCheckService(IEmailLogService emailLogService)
        {
            _emailLogService = emailLogService;
        }

        public async Task<bool> HealthCheckTodayAsync()
        {
            var dateNow = DateTime.UtcNow;

            var emailLogs = (await _emailLogService.GetAsync(dateNow.Year, dateNow.Month, dateNow.Day)).ToList();

            if (!emailLogs.Any())
                return false;

            if (emailLogs.Any(a => a.StatusEnum == EmailerStatusEnum.Failed))
                return false;

            return true;
        }

        public async Task<bool> HealthCheckTodayAsync(int forLastMinutes)
        {
            var dateNow = DateTime.UtcNow;

            var emailLogs = (await _emailLogService.GetAsync(dateNow.Year, dateNow.Month, dateNow.Day))
                .Where(w => w.CreatedDate >= dateNow.AddMinutes(Math.Abs(forLastMinutes) * -1)).ToList();

            if (!emailLogs.Any())
                return false;

            if (emailLogs.Any(a => a.StatusEnum == EmailerStatusEnum.Failed))
                return false;

            return true;
        }
    }
}