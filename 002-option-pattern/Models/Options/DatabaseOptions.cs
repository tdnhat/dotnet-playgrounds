using System.ComponentModel.DataAnnotations;

namespace _002_option_pattern.Models.Options
{
    public class DatabaseOptions
    {
        public const string SectionName = "DatabaseOptions";

        [Required(ErrorMessage = "Connection string is required.")]
        [MinLength(10, ErrorMessage = "Connection string must be at least 10 characters long.")]
        [MaxLength(500, ErrorMessage = "Connection string must not exceed 500 characters.")]
        [RegularExpression(@"^(Server|Data Source)=.+;Database=.+;User Id=.+;Password=.+;", ErrorMessage = "Invalid connection string format.")]
        public string ConnectionString { get; set; } = string.Empty;
        [Range(1, 100, ErrorMessage = "Max pool size must be between 1 and 100.")]
        public int TimeoutSeconds { get; set; } = 30;
        public bool EnableRetry { get; set; } = true;
        [Range(1, 10, ErrorMessage = "Max retry attempts must be between 1 and 10.")]
        [Required(ErrorMessage = "Max retry attempts is required.")]
        public int MaxRetryAttempts { get; set; } = 3;
    }
}