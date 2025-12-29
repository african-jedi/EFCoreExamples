# EF Core Examples Repository

A comprehensive collection of practical examples demonstrating Entity Framework Core implementations across different database platforms and design patterns.

## Overview

This repository contains multiple projects showcasing Entity Framework Core (EF Core 9.0) with both Code-First and Database-First approaches. Each project demonstrates real-world patterns, best practices, and common solutions for working with different database systems.

## Projects

### 1. SQL Server Code-First (`SQLServer.CodeFirst.EFCore`)

**Description:** Demonstrates Entity Framework Core Code-First approach with SQL Server.

**Key Features:**
- Model-driven database design
- Entity relationships (Product-Category)
- Migration management
- CRUD operations with async/await patterns
- Shadow properties configuration
- DbContext configuration and dependency injection

**Getting Started:**
1. Install SQL Server Express or use SQL Server LocalDB
2. Install EF Core CLI: `dotnet tool install --global dotnet-ef`
3. Configure connection string in `CodeFirstDBContext`
4. Run migrations: `dotnet ef database update`

**Technologies:**
- Entity Framework Core 9.0
- SQL Server
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

---

### 2. SQL Server Database-First (`SQLServer.DatabaseFirst.EFCore`)

**Description:** Demonstrates Entity Framework Core Database-First approach with SQL Server, generating models from existing database schemas.

**Key Features:**
- Automatic model scaffolding from existing databases
- DbContext generation from database schema
- Handling circular references with `[JsonIgnore]` attributes
- Configuration-based connection strings (appsettings.json)
- Dependency injection patterns
- Executing stored procedures with `FromSqlRaw()`
- Listing all database queries and execution analysis

**Use Cases:**
- Integrating with legacy database systems
- Rapid prototyping with existing schemas
- Database schema versioning and updates

**Technologies:**
- Entity Framework Core 9.0
- SQL Server
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.DependencyInjection

---

### 3. PostgreSQL Code-First (`PostgreSQL.Codefirst.EFCore`)

**Description:** Demonstrates Entity Framework Core Code-First approach with PostgreSQL, utilizing the Npgsql provider.

**Key Features:**
- PostgreSQL-specific EF Core integration
- Model configuration and entity relationships
- Migration workflows for PostgreSQL
- Connection string configuration for remote/local databases
- Npgsql database provider integration

**Getting Started:**
1. Ensure PostgreSQL is installed and running (default port: 5432)
2. Install required packages:
   - `Npgsql.EntityFrameworkCore.PostgreSQL`
   - `Microsoft.EntityFrameworkCore.Tools`
3. Configure connection string
4. Run migrations: `dotnet ef database update`

**Technologies:**
- Entity Framework Core 9.0
- PostgreSQL
- Npgsql.EntityFrameworkCore.PostgreSQL
- Microsoft.EntityFrameworkCore.Tools

---

### 4. Common Models (`Common/EFCore.CodeFirst.Models`)

**Description:** Shared domain models used across projects for code reuse and consistency.

**Models:**
- **Product** - Represents a product with pricing and category association
- **Category** - Represents product categories

These shared models demonstrate:
- Entity relationships (Product → Category)
- Data annotations and validation
- Reusable model definitions across multiple database platforms

---

## Project Structure

```
EFCoreExamples/
├── src/
│   ├── Common/
│   │   └── EFCore.CodeFirst.Models/          # Shared domain models
│   │       ├── Product.cs
│   │       └── Category.cs
│   ├── SQLServer.CodeFirst.EFCore/           # SQL Server Code-First example
│   │   ├── Program.cs
│   │   ├── Data/
│   │   │   └── CodeFirstDBContext.cs
│   │   ├── Migrations/
│   │   └── README.CodeFirst.md
│   ├── SQLServer.DatabaseFirst.EFCore/       # SQL Server Database-First example
│   │   ├── Program.cs
│   │   ├── Context/
│   │   ├── Models/
│   │   ├── appsettings.json
│   │   └── README.md
│   ├── PostgreSQL.Codefirst.EFCore/          # PostgreSQL Code-First example
│   │   ├── PostgreSQL.CodeFirst.Example/
│   │   │   ├── Program.cs
│   │   │   ├── Data/
│   │   │   ├── Migrations/
│   │   │   └── README.CodeFirstPostgreSQL.md
│   └── EFCoreExamples.sln                    # Solution file
└── README.md                                  # This file
```

---

## Prerequisites

### Global Requirements
- .NET 9.0 SDK
- Visual Studio Code (or any compatible IDE)
- EF Core CLI Tool: `dotnet tool install --global dotnet-ef`

### Database-Specific Requirements

| Project | Database | Requirement |
|---------|----------|-------------|
| SQL Server Code-First | SQL Server | SQL Server Express or LocalDB |
| SQL Server Database-First | SQL Server | Existing SQL Server database |
| PostgreSQL Code-First | PostgreSQL | PostgreSQL 12+ installed and running |

---

## Common Workflows

### Creating and Applying Migrations

**Code-First Approach:**
```bash
# Generate migration from model changes
dotnet ef migrations add MigrationName

# Apply migration to database
dotnet ef database update

# Remove last migration (if not applied)
dotnet ef migrations remove
```

### Scaffolding from Existing Database

**Database-First Approach:**
```bash
# Generate DbContext and models from existing database
dotnet ef dbcontext scaffold "ConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o Models -c DbContextName

# Update with --force flag to overwrite existing files
dotnet ef dbcontext scaffold "ConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o Models -c DbContextName --force
```

### Configuration Best Practices

**Using appsettings.json:**
```csharp
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = config.GetConnectionString("DefaultConnection");
```

---

## Key Concepts Demonstrated

- **DbContext Configuration** - Custom context configuration and lifecycle management
- **Entity Relationships** - One-to-many relationships between entities
- **Migrations** - Creating and managing database schema changes
- **Async/Await Patterns** - Asynchronous database operations
- **LINQ Queries** - Query composition and execution
- **Dependency Injection** - Registering and injecting DbContext
- **JSON Serialization** - Handling circular references in API responses
- **Shadow Properties** - Configuration of non-mapped properties
- **Stored Procedures** - Executing stored procedures with `FromSqlRaw()`

---

## Contributing

When adding new examples:
1. Follow the existing project structure
2. Include a detailed README explaining the approach
3. Add meaningful comments to code
4. Ensure all examples are executable with proper setup instructions

---

## License

This repository is provided as educational material for learning Entity Framework Core.