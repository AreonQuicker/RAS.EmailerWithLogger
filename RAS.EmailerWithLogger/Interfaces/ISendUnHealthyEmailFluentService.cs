using System.Threading.Tasks;

namespace RAS.EmailerWithLogger.Interfaces
{
    public interface ISendUnHealthyEmailFluentService
    {
        ISendUnHealthyEmailFluentService WithSystemName(string systemName);
        ISendUnHealthyEmailFluentService WithToEmails(params string[] toEmails);
        ISendUnHealthyEmailFluentService WithForLastMinutes(int forLastMinutes);
        Task SendAsync();
    }
}