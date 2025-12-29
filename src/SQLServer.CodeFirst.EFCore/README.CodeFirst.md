# Setting Up Entity Framework Core Code-First

## Prerequisites

### 1. SQL Server Express
Install SQL Server Express (free edition):
- Download from: https://www.microsoft.com/en-us/sql-server/sql-server-express
- Or use SQL Server LocalDB (included with Visual Studio)

### 2. EF Core CLI Tool
Install the EF Core CLI tool globally (one-time setup):
```bash
dotnet tool install --global dotnet-ef
```

## Required NuGet Packages

Add the following packages to your project:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 9.0.9
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### Package Descriptions

**Microsoft.EntityFrameworkCore.SqlServer**
- Provides SQL Server-specific EF Core provider for database connectivity and SQL generation

**Microsoft.EntityFrameworkCore.Tools**
- Enables EF Core command-line and Package Manager Console commands (Add-Migration, Update-Database, etc.)
- Required for local development workflows

**Microsoft.EntityFrameworkCore.Design**
- Provides design-time services for EF Core tooling
- Enables migration generation and scaffolding capabilities
- Development-only; not required at runtime

## Workflow Steps

### Step 1: Create Models
Define your data model classes. These represent the tables and columns in your database. Example models include Product and Category:

Product class code:
```csharp
public class Product
{
    public int RowId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
}
```
Category class code:
```csharp
public class Category
{
    public int RowId { get; set; }
    public string CategoryName { get; set; } = null!;   
}
```

### Step 2: Create DbContext
1. Create a class that extends `DbContext` from Entity Framework Core
2. Add a `DbSet<TEntity>` property for each model class—EF Core uses these to create database tables
3. Override the `OnConfiguring()` method to specify your SQL Server connection string
4. Use the `UseSqlServer()` extension method (provided by Microsoft.EntityFrameworkCore.SqlServer)

### Step 3: Create Initial Migration

Generate your first migration file:

**CLI:**
```bash
dotnet ef migrations add InitialMigration
```

**Package Manager Console (Visual Studio):**
```powershell
Add-Migration InitialMigration
```

This creates a migration file containing the schema definition based on your DbContext and models.

### Step 4: Update the Database

Apply the migration to create the database and tables:

**CLI:**
```bash
dotnet ef database update
```

**Package Manager Console (Visual Studio):**
```powershell
Update-Database
```

## Managing Migrations

### Create Additional Migrations

After modifying your models, create a new migration to track the changes:

**CLI:**
```bash
dotnet ef migrations add AddNewFeature
```

**Package Manager Console:**
```powershell
Add-Migration AddNewFeature
```

### Remove the Last Migration

If you need to undo the last migration before applying it to the database:

**CLI:**
```bash
dotnet ef migrations remove
```

**Package Manager Console:**
```powershell
Remove-Migration
```

> **Note:** You can only remove migrations that haven't been applied to the database.