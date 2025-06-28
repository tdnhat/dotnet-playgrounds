using System.ComponentModel.DataAnnotations;

namespace _003_application_configuration.Models.Options
{
    public class ApplicationOptions
    {
        public const string SectionName = "Application";
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Version { get; set; } = "1.0.0";
    }
}