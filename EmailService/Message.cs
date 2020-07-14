using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailService
{
    public class Message
    {
        public EmailAddress To { get; set; }
        public string Subject { get; set; }
        public string PlainContent { get; set; }
        public string HtmlContent { get; set; }

        public Message(string to, string subject, string plainContent, string htmlContent)
        {
            To = new EmailAddress(to);
            Subject = subject;
            PlainContent = plainContent;
            HtmlContent = htmlContent;
        }
    }
}
