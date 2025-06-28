# Screencast Script: App Startup and Configuration in .NET

## Introduction (30 seconds)

Hello! I'm Nhat, and today I'm demonstrating what I've learned and applied regarding Application Startup and Configuration in .NET. I'll walk through a practical implementation that showcases key concepts.

**Today's Focus:**
- Application Lifecycle and Startup Process
- Configuration Management Best Practices

In the next 6-7 minutes, I'll show how these concepts are applied in a real working application.

## Application Lifecycle and Startup Process (1 minute)

I've focused on understanding how .NET applications initialize and configure themselves. Let me show the `003-application-configuration` project I built to demonstrate these concepts:

**Key Concepts Applied:**
- ✅ Program.cs as application entry point (no Startup.cs in modern .NET)
- ✅ Configuration Sources integration
- ✅ Service Registration with dependency injection
- ✅ Middleware Pipeline configuration
- ✅ Environment Management implementation

**Project Structure Created:**
```
003-application-configuration/
├── Program.cs (Application entry point)
├── Models/Options/ (Strongly-typed config classes)
├── Endpoints/ (Organized feature endpoints)
├── Utils/ (Security helpers)
├── appsettings.json (Base configuration)
├── appsettings.Development.json (Dev overrides)
└── appsettings.Production.json (Production overrides)
```

This structure directly implements the concepts for robust application setup.

## Demo: Program.cs - Application Entry Point and Configuration (1.5 minutes)

Program.cs is the modern application entry point. Here's how the application lifecycle is implemented:

```csharp
using _003_application_configuration.Models.Options;
using _003_application_configuration.Utils;
using _003_application_configuration.Endpoints;
using Microsoft.Extensions.Options;

// Step 1: Application Initialization
var builder = WebApplication.CreateBuilder(args);
```

**This step handles:**
- Configuration sources loading (appsettings.json, environment variables, command line)
- Logging infrastructure setup
- Environment detection (Development/Production)

```csharp
// Step 2: Service Registration (Dependency Injection Setup)
builder.Services.AddOpenApi();

// Applying the strongly-typed configuration pattern
builder.Services.AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration.GetSection(ApplicationOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

Here, services are registered in the DI container, and configuration is bound to strongly-typed objects with validation to ensure correctness at startup.

```csharp
// Step 3: Application Build
var app = builder.Build();

// Step 4: Middleware Pipeline Configuration
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Development-specific middleware
}

// Step 5: Request Processing Pipeline
app.MapGeneralEndpoints();
app.MapApplicationEndpoints();
app.MapDatabaseEndpoints();

app.UseHttpsRedirection();
app.Run(); // Start listening for requests
```

The middleware pipeline configures how each request flows through the application, with environment-specific behaviors applied.

## Configuration Management Best Practices (1.5 minutes)

Implementing secure and maintainable configuration patterns is crucial. Here's how the Options Pattern is applied for strongly-typed configuration:

```csharp
// ApplicationOptions.cs - Strongly-typed configuration pattern
public class ApplicationOptions
{
    public const string SectionName = "Application"; // Avoiding magic strings
    
    [Required(ErrorMessage = "Application name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Version is required")]
    public string Version { get; set; } = "1.0.0";
}

// DatabaseOptions.cs - Security-focused configuration
public class DatabaseOptions
{
    public const string SectionName = "Database";

    [Required]
    [MinLength(5)]
    public string ConnectionString { get; set; } = string.Empty;
    
    [Range(1, 60, ErrorMessage = "Timeout must be 1-60 seconds")]
    public int TimeoutSeconds { get; set; } = 30;
    
    public bool EnableRetry { get; set; } = true;
    
    [Range(1, 10)]
    public int MaxRetryAttempts { get; set; } = 5;
}
```

**This implementation demonstrates:**
- ✅ **Strongly Typed Configuration**: Eliminating magic strings and casting
- ✅ **Validation**: Data annotations ensure configuration validity at startup
- ✅ **Default Values**: Providing safe fallbacks when configuration is missing
- ✅ **Section Names**: Using constants for improved maintainability

## Demo: Configuration Hierarchy and Environment-Specific Settings (1.5 minutes)

Understanding Configuration Hierarchy and Override Precedence is vital. Here's how environment-specific settings are implemented:

**Base Configuration (appsettings.json):**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Application": {
    "Name": "My Default Application",
    "Version": "1.0.0"
  },
  "Database": {
    "ConnectionString": "",
    "TimeoutSeconds": 30,
    "EnableRetry": true,
    "MaxRetryAttempts": 5
  }
}
```

**Development Overrides (appsettings.Development.json):**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"  // More verbose logging for development
    }
  },
  "Application": {
    "Name": "My Development Application",  // Clear environment indication
    "Version": "2.0.0-dev"
  },
  "Database": {
    "ConnectionString": "Server=localhost;Database=DevDB;Trusted_Connection=true;",
    "TimeoutSeconds": 60  // Longer timeout for debugging
  }
}
```

**The configuration source priority is:**
1. appsettings.json (Base configuration)
2. appsettings.{Environment}.json (Environment overrides)
3. Environment Variables (Deployment-specific)
4. Command Line Arguments (Highest priority)

This enables flexible environment management: Development uses verbose logging, longer timeouts, and a local database, while Production uses minimal logging, strict timeouts, and secure connection strings from environment variables.

## Demo: Secret Management and Secure Configuration Handling (1.5 minutes)

Secret Management is a critical security practice. Here's how sensitive configuration data is handled securely:

**Security Helper Implementation:**
```csharp
// Helpers.cs - Security best practices
public static class Helpers
{
    public static string MaskConnectionString(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            return "No connection string configured";

        // Masking sensitive keys
        var sensitiveKeys = new[] { 
            "password", "pwd", "user id", "uid", "username",
            "token", "key", "secret", "auth"
        };
        
        var result = connectionString;
        foreach (var key in sensitiveKeys)
        {
            var pattern = $@"({key}\s*=\s*)[^;]*";
            result = Regex.Replace(result, pattern, "$1***", RegexOptions.IgnoreCase);
        }
        return result;
    }
}
```

**Secure Endpoint Implementation:**
```csharp
// DatabaseEndpoints.cs - Never expose raw sensitive data
public static class DatabaseEndpoints
{
    public static void MapDatabaseEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/database-info", (IOptions<DatabaseOptions> options) =>
        {
            var configDetails = new
            {
                // Using helper to mask sensitive connection string
                ConnectionString = Helpers.MaskConnectionString(options.Value.ConnectionString),
                TimeoutSeconds = options.Value.TimeoutSeconds,
                EnableRetry = options.Value.EnableRetry,
                MaxRetryAttempts = options.Value.MaxRetryAttempts,
            };
            return Results.Ok(configDetails);
        })
        .WithName("GetDatabaseInfo")
        .WithTags("Database");
    }
}
```

**Key principles for secret management include:**
- ✅ Never storing secrets in source control (appsettings.json has empty connection strings)
- ✅ Using environment variables for production secrets
- ✅ Masking sensitive data in logs and API responses
- ✅ Validating configurations without exposing sensitive data in validation errors

## Live Demo: Demonstrating Concepts in Action (1 minute)

Let me demonstrate how these concepts work together in a running application:

**[During recording: Run these commands]**

```bash
# Testing Application Lifecycle - Development Environment
dotnet run --environment Development
```

**Testing Configuration Sources and Hierarchy:**

1. **GET /app-info** - Shows strongly-typed configuration in action:
   ```json
   {
     "name": "My Development Application",  // From appsettings.Development.json
     "version": "2.0.0-dev"                // Override from environment config
   }
   ```

2. **GET /database-info** - Demonstrates secret management:
   ```json
   {
     "connectionString": "Server=localhost;Database=DevDB;User ID=***;Password=***;",
     "timeoutSeconds": 60,  // Development-specific longer timeout
     "enableRetry": true,
     "maxRetryAttempts": 5
   }
   ```

**Testing Environment Management:**
```bash
# Switch to Production environment
dotnet run --environment Production
```

**Production Configuration Response:**
- Application name changes to "My Production Application"
- Timeout reduces to 10 seconds (production optimization)
- Connection string still masked for security

## Summary: Key Learning Outcomes (45 seconds)

I've successfully demonstrated all the essential concepts:

**Application Lifecycle and Startup Process ✅**
- ✅ Modern Program.cs entry point implementation
- ✅ Configuration sources integration (appsettings.json, environment variables, command line)
- ✅ Proper service registration with dependency injection
- ✅ Organized middleware pipeline with environment-specific behavior
- ✅ Working multi-environment management

**Configuration Management Best Practices ✅**
- ✅ Configuration hierarchy with proper override precedence
- ✅ Strongly-typed configuration using Options pattern
- ✅ Comprehensive secret management and data masking
- ✅ Environment-specific settings for different deployment scenarios

**Key Takeaways:**
- Configuration hierarchy enables flexible deployments without hard-coded values
- Strongly-typed configuration catches errors at startup, not runtime
- Security must be built-in from the start, not added later
- Clean architecture principles apply even to configuration code

This implementation follows enterprise-grade patterns suitable for production deployment. The complete code is available in my dotnet-playgrounds repository.

Thank you for watching my demonstration of these key .NET configuration concepts!