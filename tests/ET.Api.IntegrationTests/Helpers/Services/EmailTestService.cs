using System.Threading.Tasks;
using ET.Application.Common.Email;
using ET.Application.Services;

namespace ET.Api.IntegrationTests.Helpers.Services;

public class EmailTestService : IEmailService
{
    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        await Task.Delay(100);
    }
}
