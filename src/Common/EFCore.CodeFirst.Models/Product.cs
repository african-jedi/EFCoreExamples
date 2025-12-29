using System;

namespace EFCore.CodeFirst.Models;

public class Product
{
    public int RowId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
}
