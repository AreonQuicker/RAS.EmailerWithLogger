using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RAS.EmailerWithLogger.Interfaces;

namespace RAS.EmailerWithLogger.HealthChecks
{
    public class EmailTodayLastHourHealthCheck : IHealthCheck
    {
        private readonly IEmailerHealthCheckService _emailerHealthCheckService;

        public EmailTodayLastHourHealthCheck(IEmailerHealthCheckService emailerHealthCheckService)
        {
            _emailerHealthCheckService = emailerHealthCheckService;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var healthCheck = await _emailerHealthCheckService.HealthCheckTodayAsync(60);

            if (!healthCheck)
                return HealthCheckResult.Unhealthy("Emails for last hour are not healthy");

            return HealthCheckResult.Healthy("Emails for last last hour are healthy");
        }
    }
}