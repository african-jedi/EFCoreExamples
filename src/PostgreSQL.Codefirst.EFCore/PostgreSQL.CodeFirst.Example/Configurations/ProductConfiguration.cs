using System;
using EFCore.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostgreSQL.CodeFirst.Example.Constants;

namespace PostgreSQL.CodeFirst.Example.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Products);
        
        builder.HasKey(p => p.RowId);
        builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(100)");
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        //builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}