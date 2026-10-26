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
                From = "Ashley Nashae Photography <reviews@ashleynashaephotography.com>",
                Subject = "We'd Love to Hear About Your Experience",
                HtmlBody = $"""<a href="{reviewLink}">Leave a Review</a>"""

            };
            message.To.Add(clientEmail);

            var response = await _resend.EmailSendAsync(message);

            if (!response.Success) throw new Exception("Resend failed to send the email.");


        }

        public async Task SendTestEmailAsync()
        {
            var message = new EmailMessage
            {
                From = "Ashley Nashae Photography <reviews@ashleynashaephotography.com>",
                Subject = "Ashley Nashae Photography Test",
                HtmlBody = "<strong>It works!</strong>"
            };

            message.To.Add("ashleygrimmett@yahoo.com");

            var response = await _resend.EmailSendAsync(message);

            if (!response.Success) throw new Exception("Resend failed to send the email.");
        }
    }
}
