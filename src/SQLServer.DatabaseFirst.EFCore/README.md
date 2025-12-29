# Database-First Approach with EF Core

## Prerequisites

Install the EF Core CLI tool globally (one-time setup):
```bash
dotnet tool install --global dotnet-ef
```

## Required NuGet Packages

Add the following packages to your project:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## Scaffolding Models from Existing Database

The database-first approach generates C# models from an existing SQL Server database using the `dotnet ef` scaffold command.

### Initial Scaffold Command

Generate models from your database:

```bash
dotnet ef dbcontext scaffold "Server=localhost\SQLEXPRESS;Database=DatabaseFirstDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c DatabaseFirstDBContext -n SQLServer.DatabaseFirst.EFCore --context-dir Context
```

### Update Models After Database Changes

Regenerate models when new tables are added to the database:

```bash
dotnet ef dbcontext scaffold "Server=localhost\SQLEXPRESS;Database=DatabaseFirstDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c Math28DBContext -n DatabaseFirst.EFCore --context-dir Context --force
```

### Using Named Connection String

Alternatively, reference a connection string from your configuration:

```bash
dotnet ef dbcontext scaffold name=DefaultConnection Microsoft.EntityFrameworkCore.SqlServer -o Models -c Math28DBContext --context-dir Context --force
```

### Scaffold Command Parameters

| Parameter | Description |
|-----------|-------------|
| Connection String | Specifies the database connection details |
| `Microsoft.EntityFrameworkCore.SqlServer` | Specifies the EF Core database provider |
| `-o Models` | Output directory for generated model classes |
| `-c Math28DBContext` | Name for the generated DbContext class |
| `-n` | Namespace for generated files |
| `--context-dir` | Directory for the DbContext file |
| `--force` | Overwrites existing files (use when updating models) |

## Common Patterns and Solutions

### Loading Configuration from appsettings.json

Use `Microsoft.Extensions.Configuration` to read connection strings and other settings:

```csharp
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");
```

### Dependency Injection with DbContext

Use `Microsoft.Extensions.DependencyInjection` to initialize the DbContext with a connection string. Constructor injection is preferred over passing connection strings directly.

### Handling JsonSerializationException (Circular References)

When serializing entities to JSON, circular references cause `SerializerCycleDetected` exceptions. Fix this by adding the `[JsonIgnore]` attribute to properties causing cycles:

```csharp
using System.Text.Json.Serialization;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public virtual Category Category { get; set; }
}
```

### Reading Data from Stored Procedures

Use `FromSqlRaw()` to execute stored procedures:

```csharp
// Execute stored procedure without parameters
var puzzles = _context.GeneratedPuzzles
    .FromSqlRaw("spGetPuzzles")
    .ToList();

// Execute with parameters
var puzzles = _context.GeneratedPuzzles
    .FromSqlRaw("spGetPuzzles {0}", id)
    .ToList();
```

### Updating Data via Stored Procedures

Execute stored procedures that perform updates:

```csharp
await _context.Database.ExecuteSqlRawAsync("spUpdatePuzzle {0}, {1}", id, puzzleData);
```