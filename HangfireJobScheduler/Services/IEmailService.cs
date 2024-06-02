
namespace HangfireJobScheduler.Services;

public interface IEmailService
{
    Task SendDailyEmails();
}
