using _003_application_configuration.Models.Options;
using Microsoft.Extensions.Options;

namespace _003_application_configuration.Endpoints
{
    public static class ApplicationEndpoints
    {
        public static void MapApplicationEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/app-info", (IOptions<ApplicationOptions> options) =>
            {
                var configDetails = new
                {
                    Name = options.Value.Name,
                    Version = options.Value.Version,
                };
                return Results.Ok(configDetails);
            })
            .WithName("GetApplicationInfo")
            .WithSummary("Get application configuration details")
            .WithDescription("Returns the application name and version from configuration")
            .WithTags("Application");
        }
    }
}
