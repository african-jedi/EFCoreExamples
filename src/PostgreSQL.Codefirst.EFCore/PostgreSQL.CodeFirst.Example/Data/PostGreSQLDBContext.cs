using EFCore.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.CodeFirst.Example.Data;

public class PostGreSQLDBContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=CodeFirstDB;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostGreSQLDBContext).Assembly);
}
