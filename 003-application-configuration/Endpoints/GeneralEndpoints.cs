namespace _003_application_configuration.Endpoints
{
    public static class GeneralEndpoints
    {
        public static void MapGeneralEndpoints(this IEndpointRouteBuilder endpoints)
        {
            /// <summary>
            /// Maps the root endpoint to return a welcome message.
            /// This endpoint provides usage instructions for the application.
            /// </summary>
            endpoints.MapGet("/", () => "Welcome to Configuration Demo! Use /app-info & /database-info to see configuration details.")
                .WithName("GetWelcome")
                .WithSummary("Welcome message")
                .WithDescription("Returns a welcome message with usage instructions")
                .WithTags("General");
        }
    }
}
