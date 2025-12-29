## Setup CodeFirst PostgreSQL

## Prerequisites

### System Prerequisites
1. **PostgreSQL** installed and running (default port: 5432)
   - Download from https://www.postgresql.org/download/
   - Ensure PostgreSQL service is running

### CLI Prerequisites
1. `dotnet tool install --global dotnet-ef`
2. `Install-Package Microsoft.EntityFrameworkCore.Tools` - if using VS "Scaffold-DBContext" command

## NuGet Packages
Install the following packages to enable EF Core with PostgreSQL:
1. `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL -v 9.0.2` - PostgreSQL provider for EF Core
2. `dotnet add package Microsoft.EntityFrameworkCore.Tools -v 9.0.9` - CLI tools for migrations
3. `dotnet add package Microsoft.EntityFrameworkCore -v 9.0.9` - Core EF Framework

## Steps

### Step 1: Add Models
Create Models folder and then create a Product class.
```c#
public class Product
{
    public int RowId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
}
```

### Step 2: Create Configuration
Create configuration file for Product class and Category class.


### Step 3: Add DBContext
Create a DBContext class that will be used by EF Core to generate database schema.

**Implementation:**

1. Create a class that extends `DbContext` from EntityFrameworkCore
2. Override `OnConfiguring()` method to specify the database connection string
3. Use `UseNpgsql()` extension method from the Npgsql.EntityFrameworkCore.PostgreSQL package

**Example:**
```csharp
public class PostGreSQLDBContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CodeFirstDB;Username=postgres;Password=postgres");
    }
}
```

**Connection String Parameters:**
- `Host` - PostgreSQL server address (localhost for local development)
- `Port` - PostgreSQL port (default: 5432)
- `Database` - Database name to create/use
- `Username` - PostgreSQL user (default: postgres)
- `Password` - User password

**Best Practice:** For production, use configuration from `appsettings.json` instead of hardcoding:
```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();
    
    optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
}
```

### Step 4: Create Initial Migration
Run the following command to create a migration that captures your model definitions:
```bash
dotnet ef migrations add InitialMigration
```
Or in Visual Studio Package Manager Console:
```powershell
Add-Migration InitialMigration
```

This generates migration files in the `Migrations` folder that describe how to create the database schema.

### Step 5: Update Database
Apply the migration to create the database and tables:
```bash
dotnet ef database update
```
Or in Visual Studio Package Manager Console:
```powershell
Update-Database
```

This command:
- Creates the PostgreSQL database if it doesn't exist
- Creates all tables based on your models
- Creates the `__EFMigrationsHistory` table to track applied migrations

### Step 6: Verify Database Creation
Connect to PostgreSQL and verify the database and tables were created:
```sql
-- List all databases
\l

-- Connect to your database
\c CodeFirstDB

-- List all tables
\dt

-- View Products table structure
\d Products
```

## Next Steps

### Querying Data
```csharp
using (var context = new PostGreSQLDBContext())
{
    var products = context.Products.ToList();
    var expensiveProducts = context.Products.Where(p => p.Price > 100).ToList();
}
```

### Adding Data
```csharp
using (var context = new PostGreSQLDBContext())
{
    context.Products.Add(new Product { Name = "Laptop", Price = 999.99m });
    context.SaveChanges();
}
```

### Making Schema Changes
1. Modify your model classes
2. Create a new migration: `dotnet ef migrations add <MigrationName>`
3. Apply changes: `dotnet ef database update`

## Troubleshooting

| Issue | Solution |
|-------|----------|
| **Connection refused** | Ensure PostgreSQL is running and on the correct port (default: 5432) |
| **Authentication failed** | Verify username/password are correct |
| **Database does not exist** | Run `dotnet ef database update` to create it |
| **Migrations folder not found** | Ensure you're in the correct project directory |
| **Port already in use** | Change port in connection string or stop PostgreSQL service |

## Best Practices

1. **Never hardcode credentials** - Use configuration files or environment variables
2. **Use meaningful model names** - EF will use plural form for table names (Product → Products)
3. **Add validation** - Use data annotations on model properties
4. **Use async operations** - For better performance with `ToListAsync()`, `SaveChangesAsync()`
5. **Keep migrations clean** - Review migration files before applying to production