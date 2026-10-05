using Microsoft.AspNetCore.Mvc;
using Resend;

namespace AshleyNashaePhotography.Web.Services
{
    public class EmailService
    {
        private readonly IResend _resend;

        public EmailService(IResend resend)
        {
            _resend = resend;
        }

        public async Task SendReviewRequestAsync(string clientName, string clientEmail,
            string reviewLink)
        {
            var message = new EmailMessage
            {
                From = "Ashley Nashae Photography <onboarding@resend.dev>",
                Subject = "We'd Love to Hear About Your Experience",
                HtmlBody = "<p>This is a test review request.</p>"

            };
            message.To.Add(clientEmail);

            await _resend.EmailSendAsync(message);
        }

        public async Task SendTestEmailAsync()
        {
            var message = new EmailMessage
            {
                From = "Ashley Nashae Photography <onboarding@resend.dev>",
                Subject = "Ashley Nashae Photography Test",
                HtmlBody = "<strong>It works!</strong>"
            };

            message.To.Add("dkhammond1989@gmail.com");

            await _resend.EmailSendAsync(message);
        }
    }
}
