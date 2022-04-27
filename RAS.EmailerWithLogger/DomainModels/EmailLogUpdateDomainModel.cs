using System;
using E.S.Data.Query.Context.Attributes;

namespace RAS.EmailerWithLogger.DomainModels
{
    [DataQueryContext("EmailLog", "Emailer")]
    public class EmailLogUpdateDomainModel
    {
        public DateTime? CreatedDate { get; init; }
        public string TemplateId { get; init; }
        public string Subject { get; init; }
        public string ToEmails { get; init; }
        public string Status { get; init; }
        public string LoggedInUser { get; init; }
    }
}