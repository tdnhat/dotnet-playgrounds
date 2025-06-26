using _002_option_pattern.Services;
using Microsoft.AspNetCore.Mvc;

namespace _002_option_pattern.Endpoints
{
    public static class NotificationEndpoints
    {
        public static void MapNotificationEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/notification/config", async ([FromServices] NotificationService notificationService) =>
            {
                var config = await notificationService.GetNotificationConfigAsync();
                return Results.Ok(new
                {
                    Type = "IOptionsSnapshot",
                    Config = config,
                    Description = "Scoped - updated per request",
                });
            });
        }
    }
}