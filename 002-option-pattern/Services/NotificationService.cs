using _002_option_pattern.Models.Options;
using Microsoft.Extensions.Options;

namespace _002_option_pattern.Services
{
    public class NotificationService
    {
        private readonly IOptionsSnapshot<EmailOptions> _optionsSnapshot;
        public NotificationService(IOptionsSnapshot<EmailOptions> options)
        {
            _optionsSnapshot = options;
        }

        public async Task<string> GetNotificationConfigAsync()
        {
            var options = _optionsSnapshot.Value;
            return $"From: {options.FromEmail}";
        }
    }
}