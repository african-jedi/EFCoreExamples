using System;
using System.Collections.Generic;

namespace SQLServer.DatabaseFirst.EFCore;

public partial class Product
{
    public int RowId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public virtual Category Category { get; set; } = null!;
}
