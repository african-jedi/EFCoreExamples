using System;
using System.Collections.Generic;

namespace SQLServer.DatabaseFirst.EFCore;

public partial class Category
{
    public int RowId { get; set; }

    public string CategoryName { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
