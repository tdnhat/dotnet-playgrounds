# .NET CLI Commands Reference

A comprehensive guide to essential .NET CLI commands for project development.

## Project Creation Commands

### Basic Project Types
```powershell
# Console Application
dotnet new console -n MyConsoleApp

# Web API
dotnet new webapi -n MyWebApi

# Class Library
dotnet new classlib -n MyClassLibrary

# Blazor Server App
dotnet new blazorserver -n MyBlazorServerApp

# Blazor WebAssembly App
dotnet new blazorwasm -n MyBlazorWasmApp

# MVC Web Application
dotnet new mvc -n MyMvcApp

# Minimal Web API
dotnet new web -n MyMinimalApi

# Windows Forms App
dotnet new winforms -n MyWinFormsApp

# WPF Application
dotnet new wpf -n MyWpfApp

# Worker Service
dotnet new worker -n MyWorkerService
```

### Test Projects
```powershell
# xUnit Test Project
dotnet new xunit -n MyTests

# NUnit Test Project
dotnet new nunit -n MyNUnitTests

# MSTest Project
dotnet new mstest -n MyMSTests
```

## Solution Management

```powershell
# Create a new solution
dotnet new sln -n MySolution

# Add project to solution
dotnet sln add ./MyProject/MyProject.csproj

# Add multiple projects to solution
dotnet sln add **/*.csproj

# Remove project from solution
dotnet sln remove ./MyProject/MyProject.csproj

# List all projects in solution
dotnet sln list
```

## Package Management

```powershell
# Add NuGet package
dotnet add package PackageName

# Add specific version of package
dotnet add package PackageName --version 1.2.3

# Add package to specific project
dotnet add ./MyProject/MyProject.csproj package PackageName

# Remove package
dotnet remove package PackageName

# Restore all packages
dotnet restore

# Clear NuGet cache
dotnet nuget locals all --clear

# List package references
dotnet list package

# List outdated packages
dotnet list package --outdated

# Update packages
dotnet add package PackageName --version Latest
```

## Building and Running

```powershell
# Build project
dotnet build

# Build with specific configuration
dotnet build --configuration Release

# Build for specific framework
dotnet build --framework net8.0

# Clean build artifacts
dotnet clean

# Run the project
dotnet run

# Run with arguments
dotnet run -- arg1 arg2

# Run specific project
dotnet run --project ./MyProject/MyProject.csproj

# Watch for changes and auto-restart
dotnet watch run

# Watch and run tests
dotnet watch test
```

## Testing Commands

```powershell
# Run all tests
dotnet test

# Run tests with verbose output
dotnet test --verbosity normal

# Run tests and collect coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test
dotnet test --filter "TestMethodName"

# Run tests in specific class
dotnet test --filter "ClassName"

# Run tests with specific category
dotnet test --filter "Category=Integration"

# Run tests and generate report
dotnet test --logger trx --results-directory ./TestResults/
```

## Publishing and Deployment

```powershell
# Publish for current platform
dotnet publish

# Publish for specific runtime
dotnet publish -r win-x64

# Self-contained deployment
dotnet publish -r win-x64 --self-contained

# Framework-dependent deployment
dotnet publish -r win-x64 --self-contained false

# Single file deployment
dotnet publish -r win-x64 --self-contained -p:PublishSingleFile=true

# Trimmed deployment (smaller size)
dotnet publish -r win-x64 --self-contained -p:PublishTrimmed=true

# Ready-to-run deployment (faster startup)
dotnet publish -r win-x64 --self-contained -p:PublishReadyToRun=true

# Publish to specific folder
dotnet publish -o ./publish-output/
```

## Information and Diagnostics

```powershell
# Show .NET version
dotnet --version

# List installed SDKs
dotnet --list-sdks

# List installed runtimes
dotnet --list-runtimes

# Show system information
dotnet --info

# List available project templates
dotnet new list

# Search for project templates
dotnet new search web

# Get help for specific command
dotnet build --help

# Show project information
dotnet list reference
```

## Tool Management

```powershell
# Install global tool
dotnet tool install -g ToolName

# Update global tool
dotnet tool update -g ToolName

# Uninstall global tool
dotnet tool uninstall -g ToolName

# List installed tools
dotnet tool list -g

# Install local tool
dotnet tool install ToolName

# Restore tools from manifest
dotnet tool restore
```

## Development Workflow Examples

### Creating a New Web API Project
```powershell
# Create solution and project
dotnet new sln -n MyWebApiSolution
dotnet new webapi -n MyWebApi
dotnet sln add ./MyWebApi/MyWebApi.csproj

# Add common packages
cd MyWebApi
dotnet add package Swashbuckle.AspNetCore
dotnet add package Serilog.AspNetCore

# Create test project
cd ..
dotnet new xunit -n MyWebApi.Tests
dotnet sln add ./MyWebApi.Tests/MyWebApi.Tests.csproj
cd MyWebApi.Tests
dotnet add reference ../MyWebApi/MyWebApi.csproj
```

### Running Development Environment
```powershell
# Start development server with hot reload
dotnet watch run

# Start with specific environment
dotnet run --environment Development

# Start with HTTPS
dotnet run --urls "https://localhost:5001"
```

## Useful Aliases and Shortcuts

```powershell
# Quick project creation
dotnet new console -n MyApp && cd MyApp

# Build and run in one command
dotnet run --project ./MyProject/

# Test with file watcher
dotnet watch test --project ./MyProject.Tests/
```

## Common RuntimeIdentifiers (RIDs)

- `win-x64` - Windows 64-bit
- `win-x86` - Windows 32-bit
- `win-arm64` - Windows ARM64
- `linux-x64` - Linux 64-bit
- `linux-arm64` - Linux ARM64
- `osx-x64` - macOS Intel
- `osx-arm64` - macOS Apple Silicon

## Notes

- Always run `dotnet restore` after cloning a repository
- Use `dotnet watch` for development to enable hot reload
- Use `--self-contained` for deployment without .NET runtime dependency
- Use `--configuration Release` for production builds
- Test regularly with `dotnet test` during development