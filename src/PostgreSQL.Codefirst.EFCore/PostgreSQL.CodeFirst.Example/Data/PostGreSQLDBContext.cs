using Microsoft.EntityFrameworkCore;

namespace PostgreSQL.CodeFirst.Example.Data;

public class PostGreSQLDBContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=CodeFirstDB;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostGreSQLDBContext).Assembly);
}
