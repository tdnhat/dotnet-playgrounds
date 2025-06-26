using _003_application_configuration.Models.Options;
using _003_application_configuration.Utils;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration.GetSection(ApplicationOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Welcome to Configuration Demo! Use /app-info & /database-info to see configuration details.");

app.MapGet("/app-info", (IOptions<ApplicationOptions> options) =>
{
    var configDetails = new
    {
        Name = options.Value.Name,
        Version = options.Value.Version,
    };
    return Results.Ok(configDetails);
});

app.MapGet("/database-info", (IOptions<DatabaseOptions> options) =>
{
    var configDetails = new
    {
        ConnectionString = Helpers.MaskConnectionString(options.Value.ConnectionString),
        TimeoutSeconds = options.Value.TimeoutSeconds,
        EnableRetry = options.Value.EnableRetry,
        MaxRetryAttempts = options.Value.MaxRetryAttempts,
    };
    return Results.Ok(configDetails);
});

app.UseHttpsRedirection();

app.Run();