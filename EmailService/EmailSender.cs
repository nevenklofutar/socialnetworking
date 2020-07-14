using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await Send(emailMessage);
        }

        private SendGridMessage CreateEmailMessage(Message message)
        {
            EmailAddress from = new EmailAddress(_emailConfig.From);
            SendGridMessage emailMessage = MailHelper.CreateSingleEmail(from, message.To, message.Subject, 
                message.PlainContent, message.HtmlContent);

            return emailMessage;
        } 

        private async Task Send(SendGridMessage mailMessage)
        {
            try
            {
                var apiKey = _emailConfig.SendGridApiKey; // Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var response = await client.SendEmailAsync(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
        }
    }
}
