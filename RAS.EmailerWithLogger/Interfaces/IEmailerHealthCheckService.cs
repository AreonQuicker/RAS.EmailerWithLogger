using System.Threading.Tasks;

namespace RAS.EmailerWithLogger.Interfaces
{
    public interface IEmailerHealthCheckService
    {
        Task<bool> HealthCheckTodayAsync();
        Task<bool> HealthCheckTodayAsync(int forLastMinutes);
    }
}