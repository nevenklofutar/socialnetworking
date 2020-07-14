using System;

namespace EmailService
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SendGridApiKey { get; set; }
    }
}
