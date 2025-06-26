using System.ComponentModel.DataAnnotations;

namespace _003_application_configuration.Models.Options
{
    public class ApplicationOptions
    {
        public const string SectionName = "Application";
        [Required (ErrorMessage = "Application name is required.")]
        [MinLength(3, ErrorMessage = "Application name must be at least 3 characters long.")]
        public string Name { get; set; } = string.Empty;

        [Required (ErrorMessage = "Version is required.")]
        public string Version { get; set; } = "1.0.0";
    }
}