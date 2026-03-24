using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fitness_tracker.Services
{
    // A dummy implementation of IEmailSender for development.
    // Instead of sending a real email, it will log the email content to the console/Output window.
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogInformation("========== EMAIL INTERCEPTED FOR DEVELOPMENT ==========");
            _logger.LogInformation("TO: {Email}", email);
            _logger.LogInformation("SUBJECT: {Subject}", subject);
            _logger.LogInformation("BODY: {Message}", htmlMessage);
            _logger.LogInformation("=====================================================");

            // Return a completed task to simulate a successful email send operation
            return Task.CompletedTask;
        }
    }
}
