using AshleyNashaePhotography.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Resend;

namespace AshleyNashaePhotography.Web.Services
{
    public class EmailService
    {
        private readonly IResend _resend;
        private readonly IConfiguration _config;
    

        public EmailService(IResend resend, IConfiguration config)
        {
            _resend = resend;
            _config = config;
        }

        public async Task SendContactMessageAsync(string clientName, string clientEmail,
            string clientMessage)
        {
            var message = new EmailMessage
            {
                From = "Ashley Nashae Photography <bookings@ashleynashaephotography.com>",
                Subject = $"New Contact Message from {clientName}",
                HtmlBody = $"""
        <h2>New Contact Message!</h2>

        <p><strong>Name:</strong> {System.Net.WebUtility.HtmlEncode(clientName)}</p>
        <p><strong>Email:</strong> {System.Net.WebUtility.HtmlEncode(clientEmail)}</p>
        <p><strong>Message:</strong></p>
        <p style="white-space: pre-wrap;">{System.Net.WebUtility.HtmlEncode(clientMessage)}</p>
        """
            };

            message.To.Add(_config["Admin:Email"]!);

            var response = await _resend.EmailSendAsync(message);

            if (!response.Success) throw new Exception("Resend failed to send the client's contact message");
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

            if (!response.Success) throw new Exception("Resend failed to send the review request email.");
        }

        public async Task SendBookingAlertAsync(PhotoSession session)
        {
            var message = new EmailMessage
            {
                From = "Ashley Nashae Photography <bookings@ashleynashaephotography.com>",
                Subject = $"{session.FirstName} Booked a Photo Session!",
                HtmlBody = $"""
        <h2>New Session Booking!</h2>

        <p>Client: {System.Net.WebUtility.HtmlEncode(session.FirstName)} {System.Net.WebUtility.HtmlEncode(session.LastName)}</p>
        <p>Date: {session.Date:MMMM d, yyyy}</p>
        <p>Email: {System.Net.WebUtility.HtmlEncode(session.Email)}</p>
        <p>Phone: {System.Net.WebUtility.HtmlEncode(session.PhoneNumber)}</p>
        <p>Notes: {System.Net.WebUtility.HtmlEncode(session.ClientNotes)}</p>
        """
            };

            message.To.Add(_config["Admin:Email"]!);

            var response = await _resend.EmailSendAsync(message);

            if (!response.Success) throw new Exception("Resend failed to send the booking alert email.");
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
