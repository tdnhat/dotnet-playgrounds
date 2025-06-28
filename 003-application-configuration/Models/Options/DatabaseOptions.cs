using System.ComponentModel.DataAnnotations;

namespace _003_application_configuration.Models.Options
{
    public class DatabaseOptions
    {
        public const string SectionName = "Database";

        [Required]
        [MinLength(5)]
        public string ConnectionString { get; set; } = string.Empty;
        [Range(1, 60)]
        public int TimeoutSeconds { get; set; } = 30;
        public bool EnableRetry { get; set; } = true;
        [Range(1, 10)]
        public int MaxRetryAttempts { get; set; } = 5;
    }
}