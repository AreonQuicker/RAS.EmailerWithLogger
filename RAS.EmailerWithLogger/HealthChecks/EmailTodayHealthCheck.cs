using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RAS.EmailerWithLogger.Interfaces;

namespace RAS.EmailerWithLogger.HealthChecks
{
    public class EmailTodayHealthCheck : IHealthCheck
    {
        private readonly IEmailerHealthCheckService _emailerHealthCheckService;

        public EmailTodayHealthCheck(IEmailerHealthCheckService emailerHealthCheckService)
        {
            _emailerHealthCheckService = emailerHealthCheckService;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var healthCheck = await _emailerHealthCheckService.HealthCheckTodayAsync();

            if (!healthCheck)
                return HealthCheckResult.Unhealthy("Emails are not healthy for today");
            
            return HealthCheckResult.Healthy("Emails are healthy for today");
        }
    }
}