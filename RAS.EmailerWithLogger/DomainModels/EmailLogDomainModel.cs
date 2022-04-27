using System;
using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Enums;
using RAS.EmailerWithLogger.Enums;

namespace RAS.EmailerWithLogger.DomainModels
{
    [DataQueryContext("EmailLog", "Emailer")]
    public class EmailLogDomainModel
    {
        [DataQueryContextIdProperty]
        [DataQueryContextProperty(DataQueryContextPropertyFlags.None)]
        public int Id { get; init; }

        public DateTime CreatedDate { get; init; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string TemplateId { get; init; }
        public string Subject { get; init; }
        public string ToEmails { get; init; }
        public string Status { get; init; }

        [DataQueryContextProperty(DataQueryContextPropertyFlags.None)]

        public EmailerStatusEnum StatusEnum => (EmailerStatusEnum) Enum.Parse(typeof(EmailerStatusEnum), Status, true);

        public string LoggedInUser { get; init; }
    }
}