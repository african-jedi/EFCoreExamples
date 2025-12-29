using DatabaseFirst.EFCore.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SQLServer.DatabaseFirst.EFCore;

// Load configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

// Get configuration values
var connectionString = config.GetConnectionString("DefaultConnection");
var message = config.GetSection("Message").Value;

// Display loaded configuration
Console.WriteLine($"Connection String: {connectionString}");
Console.WriteLine($"Message: {message}");
Console.WriteLine();

try
{
    // Initialize database context
    using var context = new DatabaseFirstDBContext();
    
    // Create service and execute operations
    var service = new ProductsService(context);
    
    Console.WriteLine("=== Listing All Products ===");
    service.ListAll();
    
    Console.WriteLine("\n=== Using Stored Procedure ===");
    service.ListAllUsingStoredProc();

    Console.WriteLine("\n=== Using Raw SQL Query ===");
    service.ListAllUsingRawSql();

    Console.WriteLine("\n=== Using Linq query ===");
    service.ListAllUsingLinq();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

