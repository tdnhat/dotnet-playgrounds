# 📹 Video Recording Guide: App Startup & Configuration in .NET

## 🎬 Recording Setup
- **Duration**: 6-7 minutes
- **Speaker**: Nhat
- **Project**: `003-application-configuration`

---

## 📝 Section 1: Introduction (30 seconds)
### What to Say:
- "Hello! I'm Nhat, demonstrating App Startup and Configuration in .NET"
- "Today's focus: Application Lifecycle & Configuration Best Practices"
- "6-7 minutes showing real working application"

### What to Show:
- Open project in VS Code
- Show folder structure

---

## 📝 Section 2: Application Lifecycle (1 minute)
### What to Say:
- "Understanding how .NET applications initialize"
- "Key concepts I've applied..."

### What to Show:
- Project structure in explorer:
  ```
  ├── Program.cs (Entry point)
  ├── Models/Options/ (Config classes)
  ├── Endpoints/ (Organized endpoints)
  ├── Utils/ (Security helpers)
  ├── appsettings.json
  ├── appsettings.Development.json
  └── appsettings.Production.json
  ```

### Key Points to Mention:
- ✅ Program.cs as entry point (no Startup.cs)
- ✅ Configuration Sources integration
- ✅ Service Registration with DI
- ✅ Middleware Pipeline
- ✅ Environment Management

---

## 📝 Section 3: Program.cs Demo (1.5 minutes)
### What to Say:
- "Program.cs is the modern application entry point"
- "Step-by-step application lifecycle"

### What to Show:
- Open `Program.cs`
- Walk through each section:

#### Step 1: Application Initialization
```csharp
var builder = WebApplication.CreateBuilder(args);
```
**Explain**: Configuration loading, logging setup, environment detection

#### Step 2: Service Registration
```csharp
builder.Services.AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration.GetSection(ApplicationOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```
**Explain**: DI container, strongly-typed config, validation

#### Step 3: Middleware Pipeline
```csharp
app.MapGeneralEndpoints();
app.MapApplicationEndpoints();
app.MapDatabaseEndpoints();
```
**Explain**: Request processing flow, environment-specific behavior

---

## 📝 Section 4: Configuration Best Practices (1.5 minutes)
### What to Say:
- "Secure and maintainable configuration patterns"
- "Options Pattern for strongly-typed configuration"

### What to Show:
- Open `Models/Options/ApplicationOptions.cs`
- Open `Models/Options/DatabaseOptions.cs`

### Code to Highlight:
```csharp
public class ApplicationOptions
{
    public const string SectionName = "Application";
    
    [Required(ErrorMessage = "Application name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
    public string Name { get; set; } = string.Empty;
}
```

### Key Points:
- ✅ **Strongly Typed Configuration**: No magic strings
- ✅ **Validation**: Data annotations at startup
- ✅ **Default Values**: Safe fallbacks
- ✅ **Section Names**: Constants for maintainability

---

## 📝 Section 5: Configuration Hierarchy (1.5 minutes)
### What to Say:
- "Configuration Hierarchy and Override Precedence"
- "Environment-specific settings implementation"

### What to Show:
- Open `appsettings.json` (base config)
- Open `appsettings.Development.json` (overrides)

### Configuration Priority to Explain:
1. `appsettings.json` (Base)
2. `appsettings.{Environment}.json` (Environment overrides)
3. Environment Variables (Deployment-specific)
4. Command Line Arguments (Highest priority)

### Examples to Show:
- **Development**: Verbose logging, longer timeouts, local DB
- **Production**: Minimal logging, strict timeouts, secure connection strings

---

## 📝 Section 6: Secret Management (1.5 minutes)
### What to Say:
- "Secret Management is critical security practice"
- "How to handle sensitive configuration data securely"

### What to Show:
- Open `Utils/Helpers.cs`
- Open `Endpoints/DatabaseEndpoints.cs`

### Code to Highlight:
```csharp
public static string MaskConnectionString(string connectionString)
{
    var sensitiveKeys = new[] { 
        "password", "pwd", "user id", "uid", "username",
        "token", "key", "secret", "auth"
    };
    // Masking logic...
}
```

### Security Principles:
- ✅ Never store secrets in source control
- ✅ Use environment variables for production
- ✅ Mask sensitive data in logs/API responses
- ✅ Validate configs without exposing secrets

---

## 📝 Section 7: Live Demo (1 minute)
### What to Say:
- "Let me demonstrate these concepts in action"

### What to Do:
1. **Run Development Environment**:
   ```bash
   dotnet run --environment Development
   ```

2. **Test Endpoints**:
   - Navigate to `http://localhost:5000/app-info`
   - Navigate to `http://localhost:5000/database-info`
   - Show JSON responses

3. **Switch to Production**:
   ```bash
   dotnet run --environment Production
   ```
   - Show different responses

### Expected Results to Point Out:
- Application name changes per environment
- Timeout values differ
- Connection strings are masked
- Configuration hierarchy in action

---

## 📝 Section 8: Summary (45 seconds)
### What to Say:
- "Successfully demonstrated essential concepts"

### Key Points to Recap:
#### Application Lifecycle ✅
- Modern Program.cs implementation
- Configuration sources integration
- Service registration with DI
- Organized middleware pipeline
- Multi-environment management

#### Configuration Best Practices ✅
- Configuration hierarchy
- Strongly-typed Options pattern
- Secret management & data masking
- Environment-specific settings

### Final Message:
- "Enterprise-grade patterns for production"
- "Complete code in dotnet-playgrounds repository"
- "Thank you for watching!"

---

## 🎯 Quick Reference Checklist
- [ ] Project structure overview
- [ ] Program.cs walkthrough
- [ ] Options classes demo
- [ ] Configuration files comparison
- [ ] Security helpers explanation
- [ ] Live application demo
- [ ] Environment switching
- [ ] Summary of key concepts

## 📁 Files to Have Open
1. `Program.cs`
2. `Models/Options/ApplicationOptions.cs`
3. `Models/Options/DatabaseOptions.cs`
4. `appsettings.json`
5. `appsettings.Development.json`
6. `Utils/Helpers.cs`
7. `Endpoints/DatabaseEndpoints.cs`

## 🌐 URLs for Demo
- `http://localhost:5000/`
- `http://localhost:5000/app-info`
- `http://localhost:5000/database-info`
