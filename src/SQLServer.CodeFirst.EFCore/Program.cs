// See https://aka.ms/new-console-template for more information
using EFCore.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using SQLServer.CodeFirst.EFCore.Data;

Console.WriteLine("Hello, World!");

using (CodeFirstDBContext context = new CodeFirstDBContext())
{  
    Category category = new()
    {
         CategoryName="Electronics",
    };
    
     Category category2 = new()
     {
         CategoryName="Food",
    };

    Product product1 = new()
    {
         Category=category,
          Name="Iphone 17",
           Price=20000
    };

     Product product2 = new()
    {
         Category=category,
          Name="Ipad 10",
           Price=20000
    };

    await context.Products.AddAsync(product1);
    await context.Products.AddAsync(product2);
    context.SaveChanges();

    var products = context.Products.Include(c=>c.Category)
                           .AsNoTracking()
                           .ToList();
                           
    foreach (var item in products)
        Console.WriteLine($"{item.RowId}: {item.Name} - {item.Category.CategoryName}");
}
