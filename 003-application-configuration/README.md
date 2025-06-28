# Configuration Sources Guide

This guide shows how to configure application settings using different sources in order of priority (lowest to highest).

## Test Endpoints
- `GET /app-info` - View application configuration
- `GET /database-info` - View database configuration (with masked sensitive data)

## Configuration Sources (Priority Order)

### 1. appsettings.json (Lowest Priority)
```json
{
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

### 2. appsettings.Development.json
```json
{
  "Application": {
    "Name": "My Development Application",
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

### 3. User Secrets
```powershell
# Set user secrets
dotnet user-secrets set "Application:Name" "My Secret Application"
dotnet user-secrets set "Database:ConnectionString" "Server=localhost;Database=SecretDB;User Id=secret;Password=secret123;"

# View secrets
dotnet user-secrets list
```

### 4. appsettings.Production.json
```json
{
  "Application": {
    "Name": "My Production Application",
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
**Note:** Change `ASPNETCORE_ENVIRONMENT` to `"Production"` in `launchSettings.json` to use this file.

### 5. Environment Variables
```powershell
# PowerShell - Set application and database settings
$env:Application__Name = "My Environment Application"
$env:Database__TimeoutSeconds = "45"
$env:Database__ConnectionString = "Server=env-server;Database=EnvDB;User Id=envuser;Password=envpass;"
dotnet run
```

```cmd
:: Command Prompt - Set application and database settings  
set Application__Name=My Environment Application
set Database__TimeoutSeconds=45
set Database__ConnectionString=Server=env-server;Database=EnvDB;User Id=envuser;Password=envpass;
dotnet run
```

### 6. Command Line Arguments (Highest Priority)
```powershell
# Run with command line arguments for both application and database settings
dotnet run --Application:Name="My Command Line Application" --Database:TimeoutSeconds=90 --Database:MaxRetryAttempts=8
```

## Testing Different Sources

1. **Default**: Run normally → Uses Development configuration
2. **User Secrets**: Add secrets → Overrides appsettings files (great for local development)
3. **Production**: Change environment to Production → Uses Production configuration  
4. **Environment**: Set environment variables → Overrides file-based configuration
5. **Command Line**: Use command arguments → Highest priority, overrides everything

## Quick Test Commands

### PowerShell
```powershell
# 1. Default configuration (lowest priority - appsettings.json, appsettings.Development.json)
dotnet run

# 2. Production settings (set environment then run)
$env:ASPNETCORE_ENVIRONMENT = "Production"
dotnet run
$env:ASPNETCORE_ENVIRONMENT = "Development"  # Reset back when done

# 3. User secrets (overrides appsettings files)
dotnet user-secrets set "Application:Name" "SECRET TEST APP"
dotnet user-secrets set "Database:ConnectionString" "Server=secret;Database=SecretDB;User Id=secret;Password=secret123;"
dotnet run
dotnet user-secrets clear  # Optional: clear secrets when done

# 4. Environment variables (overrides user secrets and files)
$env:Application__Name = "ENV TEST APP"
$env:Database__TimeoutSeconds = "25"
$env:Database__ConnectionString = "Server=test;Database=TestDB;User Id=test;Password=test123;"
dotnet run
Remove-Item Env:\Application__Name -ErrorAction SilentlyContinue
Remove-Item Env:\Database__* -ErrorAction SilentlyContinue

# 5. Command line arguments (highest priority - overrides everything)
dotnet run --Application:Name="CMD TEST APP" --Database:MaxRetryAttempts=8
```

### Command Prompt (cmd)
```cmd
:: 1. Default configuration (lowest priority - appsettings.json, appsettings.Development.json)
dotnet run

:: 2. Production settings (set environment then run)
set ASPNETCORE_ENVIRONMENT=Production
dotnet run
set ASPNETCORE_ENVIRONMENT=Development

:: 3. User secrets (overrides appsettings files)
dotnet user-secrets set "Application:Name" "SECRET TEST APP"
dotnet user-secrets set "Database:ConnectionString" "Server=secret;Database=SecretDB;User Id=secret;Password=secret123;"
dotnet run
dotnet user-secrets clear

:: 4. Environment variables (overrides user secrets and files)
set Application__Name=ENV TEST APP
set Database__TimeoutSeconds=25  
set Database__ConnectionString=Server=test;Database=TestDB;User Id=test;Password=test123;
dotnet run
set Application__Name=
set Database__TimeoutSeconds=
set Database__ConnectionString=

:: 5. Command line arguments (highest priority - overrides everything)
dotnet run --Application:Name="CMD TEST APP" --Database:MaxRetryAttempts=8
```

## Configuration Features

- **Strongly-typed configuration** using Options pattern
- **Configuration validation** with data annotations
- **Security**: Connection strings are masked in API responses  
- **Environment-specific** settings (Development/Production)
- **User secrets** integration for sensitive development data
