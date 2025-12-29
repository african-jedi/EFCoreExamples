using System;
using EFCore.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostgreSQL.CodeFirst.Example.Constants;


namespace PostgreSQL.CodeFirst.Example.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)      
    {
        builder.ToTable(TableNames.Categories);
        builder.HasKey(c => c.RowId);
        builder.Property(c => c.CategoryName).IsRequired().HasColumnType("varchar(100)");
    }
}
