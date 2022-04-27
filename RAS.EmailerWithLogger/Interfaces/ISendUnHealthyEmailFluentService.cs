using System.Threading.Tasks;

namespace RAS.EmailerWithLogger.Services
{
    public interface ISendUnHealthyEmailFluentService
    {
        ISendUnHealthyEmailFluentService WithSystemName(string systemName);
        ISendUnHealthyEmailFluentService WithToEmails(string[] toEmails);
        ISendUnHealthyEmailFluentService WithForLastMinutes(int forLastMinutes);
        Task SendAsync();
    }
}