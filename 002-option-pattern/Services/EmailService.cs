using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _002_option_pattern.Models.Options;
using Microsoft.Extensions.Options;

namespace _002_option_pattern.Services
{
    public class EmailService
    {
        private readonly IOptionsMonitor<EmailOptions> _optionsMonitor;
        public EmailService(IOptionsMonitor<EmailOptions> options)
        {
            _optionsMonitor = options;

            _optionsMonitor.OnChange(option =>
            {
                Console.WriteLine($"Email options changed: {option}");
            });
        }

        public async Task<string> GetEmailConfigAsync()
        {
            var options = _optionsMonitor.CurrentValue;
            return $"SMTP: {options.SmtpHost}, Port: {options.SmtpPort}";
        }
    }
}