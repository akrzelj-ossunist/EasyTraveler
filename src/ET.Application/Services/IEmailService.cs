using ET.Application.Common.Email;

namespace ET.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}
