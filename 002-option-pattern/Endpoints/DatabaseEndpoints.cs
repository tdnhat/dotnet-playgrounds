using _002_option_pattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace _002_option_pattern.Endpoints
{
    public static class DatabaseEndpoints
    {
        public static void MapDatabaseEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/database/config", async ([FromServices] IDatabaseService databaseService) =>
            {
                var config = await databaseService.GetConnectionInfoAsync();
                return Results.Ok(new
                {
                    Type = "IOptions",
                    Config = config,
                    Description = "Singleton - loaded once at startup",
                });
            });
        }
    }
}