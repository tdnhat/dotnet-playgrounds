using System.ComponentModel.DataAnnotations;

namespace _003_application_configuration.Models.Options
{
    public class DatabaseOptions
    {
        public const string SectionName = "Database";

        [Required(ErrorMessage = "Connection string is required.")]
        [MinLength(5, ErrorMessage = "Connection string must be at least 5 characters long.")]
        public string ConnectionString { get; set; } = string.Empty;
        [Range(1, 60, ErrorMessage = "Timeout must be between 1 and 60 seconds.")]
        public int TimeoutSeconds { get; set; } = 30;
        public bool EnableRetry { get; set; } = true;
        [Range(1, 10, ErrorMessage = "Max retry attempts must be between 1 and 10.")]
        public int MaxRetryAttempts { get; set; } = 5;
    }
}