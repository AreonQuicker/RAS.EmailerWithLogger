using System;
using System.Net;
using System.Threading.Tasks;
using E.S.Api.Helpers.Extensions;
using E.S.Data.Query.Context.Interfaces;
using E.S.Logging.Enums;
using E.S.Logging.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RAS.Emailer.Interfaces;
using RAS.EmailerWithLogger.Constants;
using RAS.EmailerWithLogger.DomainModels;
using RAS.EmailerWithLogger.Enums;
using RAS.EmailerWithLogger.Interfaces;
using SendGrid;

namespace RAS.EmailerWithLogger.Services
{
    public class EmailerService : IEmailerService
    {
        private readonly IRepositoryService<EmailLogDomainModel> _emailLogRepositoryService;
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailerService> _logger;
        private readonly IRepositoryService<EmailLogUpdateDomainModel> _updateEmailLogRepositoryService;
        private readonly string _userName;

        public EmailerService(ILogger<EmailerService> logger, IEmailService emailService,
            IRepositoryService<EmailLogDomainModel> emailLogRepositoryService,
            IRepositoryService<EmailLogUpdateDomainModel> updateEmailLogRepositoryService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _emailService = emailService;
            _emailLogRepositoryService = emailLogRepositoryService;
            _updateEmailLogRepositoryService = updateEmailLogRepositoryService;
            _userName = httpContextAccessor.ToUserName();
            emailService.Enable();
        }

        #region IEmailerService

        public async Task<Response> SendEmailAsync(string templateId, IEmailItem emailItem, params string[] toEmails)
        {
            var emailLogId = await LogEmailAsync(templateId, emailItem, toEmails);

            try
            {
                var response = await _emailService.SendEmailAsync(emailItem,
                    templateId, toEmails);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Accepted:
                        await UpdateEmailLogAsync(emailLogId, EmailerStatusEnum.Sent);
                        break;
                    case HttpStatusCode.BadRequest:
                        LogEmailFailed(emailItem, null,
                            string.Join(",", toEmails));
                        await UpdateEmailLogAsync(emailLogId, EmailerStatusEnum.Failed);
                        break;
                    default:
                        LogEmailFailed(emailItem, null,
                            string.Join(",", toEmails));
                        await UpdateEmailLogAsync(emailLogId, EmailerStatusEnum.Failed);
                        break;
                }

                return response;
            }
            catch (Exception e)
            {
                LogEmailFailed(emailItem, e,
                    string.Join(",", toEmails));
                
                await UpdateEmailLogAsync(emailLogId, EmailerStatusEnum.Failed);
                throw;
            }
        }

        public void SetEnable(bool enable)
        {
            if (enable)
                _emailService.Enable();
            else
            {
                _emailService.Disable();
            }
        }

        #endregion

        #region Private Methods

        private async Task<int?> LogEmailAsync(string templateId, IEmailItem emailItem, string[] toEmails)
        {
            try
            {
                var createdDate = DateTime.UtcNow;

                var emailLogId = await _emailLogRepositoryService.CreateAsync(new EmailLogDomainModel
                {
                    TemplateId = templateId,
                    Subject = emailItem.Subject,
                    ToEmails = string.Join(",", toEmails),
                    Status = EmailerStatusEnum.Pending.ToString(),
                    LoggedInUser = _userName,
                    CreatedDate = createdDate,
                    Year = createdDate.Year,
                    Month = createdDate.Month,
                    Day = createdDate.Day
                });

                return emailLogId;
            }
            catch
            {
                return null;
            }
        }

        private async Task UpdateEmailLogAsync(int? emailLogId, EmailerStatusEnum status)
        {
            try
            {
                if (emailLogId.HasValue)
                    await _updateEmailLogRepositoryService.UpsertAsync(emailLogId.Value,
                        new EmailLogUpdateDomainModel {Status = status.ToString()});
            }
            catch
            {
                // ignored
            }
        }

        private void LogEmailFailed(IEmailItem emailItem, Exception e, string toEmail)
        {
            _logger.LogErrorOperation(LoggerStatusEnum.Error, LoggerConstant.System,
                null, toEmail,
                _userName, $"Sending email with subject {emailItem.Subject} to {toEmail ?? ""} failed",
                e);
        }

        #endregion
    }
}