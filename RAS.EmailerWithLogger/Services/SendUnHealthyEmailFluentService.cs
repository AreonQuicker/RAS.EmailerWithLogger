using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAS.EmailerWithLogger.Interfaces;
using RAS.EmailerWithLogger.Models;

namespace RAS.EmailerWithLogger.Services
{
    public class SendUnHealthyEmailFluentService : ISendUnHealthyEmailFluentService
    {
        private readonly IEmailerService _emailerService;
        private readonly IEmailerHealthCheckService _emailerHealthCheckService;

        private List<string> _toEmails;
        private string _systemName;
        private int _forLastMinutes;

        public SendUnHealthyEmailFluentService(IEmailerService emailerService, IEmailerHealthCheckService emailerHealthCheckService)
        {
            _emailerService = emailerService;
            _emailerHealthCheckService = emailerHealthCheckService;
        }
        
        public ISendUnHealthyEmailFluentService WithSystemName(string systemName)
        {
            _systemName = systemName;
            return this;
        }
        
        public ISendUnHealthyEmailFluentService WithToEmails(string[] toEmails)
        {
            _toEmails = toEmails.ToList();
            return this;
        }
        
        public ISendUnHealthyEmailFluentService WithForLastMinutes(int forLastMinutes)
        {
            _forLastMinutes = forLastMinutes;
            return this;
        }
        
        public async Task SendAsync()
        {
            var healthCheck = await _emailerHealthCheckService.HealthCheckTodayAsync(_forLastMinutes);
            
            if(healthCheck) 
                return;
            
            foreach (var toEmail in _toEmails)
            {
                var body = $"<p>Dear {toEmail}</p>" +
                           $"<p>{_systemName} emails are currently not responding. Please see the logs for further information. </p>" +
                           "Kind regards <br/> Credit Team";

                var template = new GenericEmailItem($"{_systemName} emails are not responding")
                {
                    Body = body,
                    Title = $"{_systemName} emails are not responding"
                };

                await _emailerService.SendEmailAsync(template, toEmail);
            }
        }
    }
}