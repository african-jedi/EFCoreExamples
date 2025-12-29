using Microsoft.EntityFrameworkCore;
using SQLServer.DatabaseFirst.EFCore;

namespace DatabaseFirst.EFCore.Service;
public class ProductsService
{
    private readonly DatabaseFirstDBContext _context;
    public ProductsService(DatabaseFirstDBContext context)
    {
        _context = context;
    }

    public void ListAll()
    {
        var items = _context.Products
                            .Include(c=> c.Category)
                            .AsNoTracking()
                            .ToList();

        foreach (var item in items)
            Console.WriteLine($"{item.RowId}: {item.Name} - {item.Category.CategoryName}");
    }
    
    // Using Product class mapped to stored procedure
    public void ListAllUsingStoredProc()
    {
        var items = _context.Products
                            .FromSqlRaw("spGetProducts")
                            .ToList();

        foreach (var item in items)
        {
            // Explicitly load the Category if it's null
            if (item.Category == null)
                _context.Entry(item).Reference(p => p.Category).Load();

            Console.WriteLine($"{item.RowId}: {item.Name} - {item.Category?.CategoryName ?? "No Category"}");
        }
    }

    public void ListAllUsingRawSql()
    {
        var items = _context.Products
                            .FromSqlRaw("SELECT p.RowId, p.Name, p.Price, p.CategoryId, p.Created, p.Modified " +
                                       "FROM Products p")
                            .Include(p => p.Category)
                            .AsNoTracking()
                            .ToList();

        foreach (var item in items)
        {
            Console.WriteLine($"{item.RowId}: {item.Name} - {item.Category?.CategoryName ?? "No Category"}");
        }
    }

    public void ListAllUsingLinq()
    {
        var items = from p in _context.Products
                    join c in _context.Categories on p.CategoryId equals c.RowId
                    select new
                    {
                        p.RowId,
                        p.Name,
                        p.Price,
                        c.CategoryName
                    };

        foreach (var item in items)
            Console.WriteLine($"{item.RowId}: {item.Name} - {item.CategoryName}");
    }
}