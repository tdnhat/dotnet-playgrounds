using _002_option_pattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace _002_option_pattern.Endpoints
{
    public static class EmailEndpoints
    {
        public static void MapEmailEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/email/config", async ([FromServices] EmailService emailService) =>
            {
                var config = await emailService.GetEmailConfigAsync();
                return Results.Ok(new
                {
                    Type = "IOptionsMonitor",
                    Config = config,
                    Description = "Singleton - updated on change",
                });
            });
        }
    }
}