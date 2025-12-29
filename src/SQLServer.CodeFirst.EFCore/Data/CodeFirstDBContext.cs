using EFCore.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace SQLServer.CodeFirst.EFCore.Data;

public class CodeFirstDBContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CodeFirstDB;Trusted_Connection=true;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
                    .HasKey(c => c.RowId);

         modelBuilder.Entity<Category>()
                    .HasKey(c => c.RowId);

        modelBuilder.Entity<Product>()
                    .Property<DateTime>("Created");

        modelBuilder.Entity<Product>()
                     .Property<DateTime>("Modified");

        modelBuilder.Entity<Category>()
                    .Property<DateTime>("Created");

        modelBuilder.Entity<Category>()
                     .Property<DateTime>("Modified");

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
                entry.Property("Created").CurrentValue = DateTime.Now;
            if (entry.State == EntityState.Modified)
                entry.Property("Modified").CurrentValue = DateTime.Now;
        }
        return base.SaveChanges();
    }
}
