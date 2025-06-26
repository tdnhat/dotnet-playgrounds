using System.ComponentModel.DataAnnotations;

namespace _002_option_pattern.Models.Options
{
    public class EmailOptions
    {
        public const string SectionName = "EmailOptions";

        [Required(ErrorMessage = "SMTP Host is required.")]
        public string SmtpHost { get; set; } = string.Empty;
        [Range(1, 65535, ErrorMessage = "SMTP Port must be between 1 and 65535.")]
        public int SmtpPort { get; set; } = 587;
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
        [Required(ErrorMessage = "From Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string FromEmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "From Name is required.")]
        public string FromName { get; set; } = string.Empty;
    }
}