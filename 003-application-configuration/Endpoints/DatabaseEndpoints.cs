using _003_application_configuration.Models.Options;
using _003_application_configuration.Utils;
using Microsoft.Extensions.Options;

namespace _003_application_configuration.Endpoints
{
    public static class DatabaseEndpoints
    {
        public static void MapDatabaseEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/database-info", (IOptions<DatabaseOptions> options) =>
            {
                var configDetails = new
                {
                    ConnectionString = Helpers.MaskConnectionString(options.Value.ConnectionString),
                    TimeoutSeconds = options.Value.TimeoutSeconds,
                    EnableRetry = options.Value.EnableRetry,
                    MaxRetryAttempts = options.Value.MaxRetryAttempts,
                };
                return Results.Ok(configDetails);
            })
            .WithName("GetDatabaseInfo")
            .WithSummary("Get database configuration details")
            .WithDescription("Returns the database configuration including masked connection string and settings")
            .WithTags("Database");
        }
    }
}
