using RAS.Emailer.Interfaces;
using RAS.Emailer.Services;

namespace RAS.EmailerWithLogger.Services
{
    public class EmailerService
    {
        private readonly IEmailService _emailService;

        public EmailerService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        
    }
}