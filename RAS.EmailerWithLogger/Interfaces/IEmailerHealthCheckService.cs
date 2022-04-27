using System.Threading.Tasks;

namespace RAS.EmailerWithLogger.Services
{
    public interface IEmailerHealthCheckService
    {
        Task<bool> HealthCheckTodayAsync();
        Task<bool> HealthCheckTodayAsync(int forLastMinutes);
    }
}