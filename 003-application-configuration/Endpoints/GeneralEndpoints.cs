namespace _003_application_configuration.Endpoints
{
    public static class GeneralEndpoints
    {
        public static void MapGeneralEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/", () => "Welcome to Configuration Demo! Use /app-info & /database-info to see configuration details.")
                .WithName("GetWelcome")
                .WithSummary("Welcome message")
                .WithDescription("Returns a welcome message with usage instructions")
                .WithTags("General");
        }
    }
}
