using System.Collections.Generic;
using RAS.Emailer.Interfaces;
using RAS.Emailer.Models;

namespace RAS.EmailerWithLogger.Models
{
    public abstract class EmailItemBase : IEmailItem
    {
        protected EmailItemBase()
        {
        }

        protected EmailItemBase(string subject)
        {
            Subject = subject;
        }

        public abstract string TemplateId { get; }
        public List<string> ToEmails { get; init; }
        public string Subject { get; set; }
        public RAS.Emailer.Models.File File { get; set; }
    }
}