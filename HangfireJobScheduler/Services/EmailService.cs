namespace HangfireJobScheduler.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendDailyEmails()
        {
            Console.WriteLine("Sending daily emails...");
            await Task.Delay(1000);
            Console.WriteLine("Daily emails sent.");
        }
    }
}
