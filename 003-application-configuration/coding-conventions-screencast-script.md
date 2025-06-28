# .NET Coding Conventions & Best Practices Screencast Script
**Duration**: 8-10 minutes | **Focus**: Code quality demonstration using real project examples

---

## 🎬 **Opening Segment** (1 minute)

### **Hook & Introduction**
> "Hi! Today I'm demonstrating .NET coding conventions and clean code principles - but instead of showing theoretical examples, I'm using a real project I built to show you **how these principles work in practice** and **why they matter for maintainable code**."

### **Problem Setup**
> "Every developer can write code that works. But can you write code that another developer can understand six months later? Can you write code that's easy to modify, test, and extend? **That's what coding conventions and clean code principles solve.**"

### **Preview**
> "In the next 8 minutes, I'll walk through this .NET configuration project and show you the specific conventions and practices that make code professional-grade."

---

## 📝 **Naming Conventions Deep Dive** (2-3 minutes)

### **Show Class and Property Naming**
```
📂 Open: Models/Options/ApplicationOptions.cs
```

**Talking Points**:
> "Look at this class name: `ApplicationOptions`. **PascalCase for classes** - every word capitalized, no spaces or underscores. This isn't arbitrary - it's the .NET standard that every C# developer expects."

```csharp
public class ApplicationOptions  // ✅ PascalCase for classes
{
    public const string SectionName = "Application";  // ✅ PascalCase for constants
    public string Name { get; set; }  // ✅ PascalCase for properties
    public string Version { get; set; }
}
```

> "**Why is consistency crucial?** When every .NET project follows the same conventions, developers can read any codebase instantly. No mental overhead wondering 'Is it applicationOptions or ApplicationOptions?'"

### **Show Method and Parameter Naming**
```
📂 Open: Utils/Helpers.cs
```

**Talking Points**:
> "Here's a perfect example of method naming:"

```csharp
public static string MaskConnectionString(string connectionString)  
//                    ↑ PascalCase           ↑ camelCase parameter
```

> "**Method names are PascalCase, parameters are camelCase**. Notice how the method name `MaskConnectionString` tells you exactly what it does - no guessing required. **This is self-documenting code.**"

### **Show Namespace Conventions**
```
📂 Show: namespace _003_application_configuration.Models.Options
```

> "Namespace structure follows the folder structure: `ProjectName.FolderName.SubFolder`. **This creates a logical hierarchy** that helps developers navigate large codebases."

---

## 🏗️ **Code Organization Excellence** (2 minutes)

### **Show Project Structure**
```
📂 Quick tour: Models/Options/, Endpoints/, Utils/
```

**Talking Points**:
> "Notice the clean separation: **Models** for data structures, **Endpoints** for HTTP concerns, **Utils** for cross-cutting functionality. **This follows the Single Responsibility Principle** - each folder has one clear purpose."

### **Show File Organization**
```
📂 Open: Endpoints/ApplicationEndpoints.cs
```

> "One endpoint type per file. **Why?** Because when you need to modify application endpoints, you know exactly where to look. Compare this to a 1000-line 'EndpointsHelper' file where endpoints are scattered everywhere."

### **Show Using Statements**
```csharp
using _003_application_configuration.Models.Options;
using Microsoft.Extensions.Options;
// Clean, organized imports at the top
```

> "**Organized using statements** - project references first, then external packages. No unused imports cluttering the file. **Clean imports = clean intent.**"

---

## 🧹 **Clean Code Principles in Action** (2-3 minutes)

### **Single Responsibility Principle**
```
📂 Open: Models/Options/DatabaseOptions.cs
```

**Talking Points**:
> "This class has **one job**: represent database configuration. It doesn't handle validation logic, doesn't manage connections, doesn't format data. **Single Responsibility Principle in practice.**"

```csharp
public class DatabaseOptions  // ✅ Only database configuration
{
    // ✅ Clear, focused properties
    public string ConnectionString { get; set; }
    public int TimeoutSeconds { get; set; }
    public bool EnableRetry { get; set; }
}
```

### **Meaningful Names**
```
📂 Open: Utils/Helpers.cs - show MaskConnectionString method
```

> "Look at this method signature: `MaskConnectionString(string connectionString)`. **You immediately understand what it does.** Compare that to a method called `ProcessData(string input)` - which tells you nothing."

### **Small, Focused Functions**
```csharp
public static string MaskConnectionString(string connectionString)
{
    if (string.IsNullOrEmpty(connectionString))
        return connectionString;
    
    // 15 lines of focused logic
}
```

> "**This function does one thing well** - masks sensitive data in connection strings. It's not trying to validate, format, and transform data all at once. **Easy to test, easy to understand, easy to maintain.**"

### **Clear Error Handling**
```
📂 Open: Program.cs - show validation setup
```

**Talking Points**:
```csharp
builder.Services.AddOptions<ApplicationOptions>()
    .Bind(builder.Configuration.GetSection(ApplicationOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();  // ✅ Fail-fast error handling
```

> "**Explicit error handling strategy**: `ValidateOnStart()` means errors are caught immediately, not buried deep in runtime execution. **The application refuses to start with bad configuration** - that's defensive programming."

---

## 📖 **Documentation Standards** (1-2 minutes)

### **Show Data Annotations as Documentation**
```
📂 Open: Models/Options/DatabaseOptions.cs
```

**Talking Points**:
```csharp
[Required]                    // ✅ Self-documenting constraint
[MinLength(5)]               // ✅ Clear validation rules  
public string ConnectionString { get; set; }

[Range(1, 60)]               // ✅ Documents valid range
public int TimeoutSeconds { get; set; }
```

> "**Data annotations serve dual purpose**: they validate data AND document constraints. Any developer reading this code immediately understands that timeout must be 1-60 seconds. **The code documents itself.**"

### **Show Endpoint Documentation**
```
📂 Open: Endpoints/ApplicationEndpoints.cs
```

**Talking Points**:
```csharp
.WithName("GetApplicationInfo")
.WithSummary("Get application configuration details")  // ✅ Clear description
.WithDescription("Returns the application name and version from configuration")
.WithTags("Application");  // ✅ Logical grouping
```

> "**Professional API documentation** built into the code. This generates OpenAPI specs automatically - **living documentation that never gets out of sync** with the actual implementation."

---

## 🎯 **Code Formatting & Consistency** (1 minute)

### **Show Consistent Formatting**
```
📂 Show multiple files quickly
```

**Talking Points**:
> "Notice the **consistent indentation, spacing, and brace placement** across all files. This isn't vanity - **consistent formatting reduces cognitive load**. Developers spend more time reading code than writing it."

### **Show Consistent Patterns**
```
📂 Show how all endpoint files follow same pattern
```

> "Every endpoint file follows the **same structure**: namespace, using statements, static class, extension method. **Predictable patterns make code scannable** - developers know where to find things."

---

## 🚀 **Real-World Impact & Best Practices** (1 minute)

### **Code Review Ready**
> "This code is **code review ready**. Clear names, obvious structure, self-documenting validation. **A senior developer can review this in minutes, not hours.**"

### **Team Scalability** 
> "**These conventions scale with team size**. Whether it's 2 developers or 20, everyone follows the same patterns. **No debates about naming, no confusion about structure.**"

### **Maintenance Excellence**
> "Six months from now, when you need to add a new configuration option, **you'll know exactly where it goes** and **exactly how to implement it** because the patterns are consistent."

### **Professional Standards**
> "**This is enterprise-grade code**. It follows .NET conventions, implements clean code principles, and demonstrates the kind of thinking that separates professional developers from beginners."

---

## 🎯 **Wrap-up & Key Takeaways** (30 seconds)

### **Summary**
> "What makes code professional isn't just that it works - **it's that it follows conventions that make it readable, maintainable, and scalable**:"
> - **Consistent naming** eliminates confusion
> - **Clear organization** makes navigation effortless  
> - **Single responsibility** makes testing and changes safe
> - **Self-documenting code** reduces maintenance overhead

### **Closing**
> "**These aren't just academic concepts** - they're practical tools that make you more effective every day. **Master these conventions, and you'll write code that other developers actually want to work with.**"

---

## 📝 **Script Notes & Tips**

### **Pacing Guidelines**
- **Move quickly between examples** - Focus on principles, not every line of code
- **Use side-by-side comparisons** when possible (good vs bad examples)
- **Point out patterns** rather than getting lost in details

### **Visual Focus**
- **Highlight specific lines** using cursor or annotations
- **Show file structure** to emphasize organization
- **Quick code navigation** to demonstrate predictable patterns

### **Emphasis Techniques**
- **Pause after key principles** - "Single Responsibility Principle"
- **Use real scenarios** - "Six months from now when you need to..."
- **Connect to professional impact** - "Code review ready", "Enterprise-grade"

---

## 🎬 **Recording Checklist**

**Before Recording**:
- [ ] All code examples compile and run
- [ ] IDE/editor has consistent formatting settings
- [ ] File structure is clean and organized
- [ ] Examples clearly demonstrate each principle

**During Recording**:
- [ ] Speak at steady pace - don't rush through examples
- [ ] Clearly identify each convention being demonstrated
- [ ] Explain "why" behind each practice
- [ ] Show real code, not theoretical examples

**Key Messages to Convey**:
- [ ] Conventions reduce cognitive load
- [ ] Consistency enables team scalability
- [ ] Clean code principles prevent technical debt
- [ ] Professional standards distinguish quality developers

---

## 💡 **Optional Enhancement Ideas**

**If Time Permits (8-minute version)**:
- Quickly show a "bad example" for contrast
- Demonstrate how conventions help with IntelliSense
- Show how organized code enables faster debugging

**Extended Version (10 minutes)**:
- Add example of refactoring messy code to clean code
- Demonstrate how conventions help with automated tooling
- Show how proper naming enables better search/navigation 