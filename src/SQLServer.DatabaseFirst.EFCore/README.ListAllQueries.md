# Different Ways to Query Products

This document outlines the various methods available in `ProductsService` to retrieve product data from the database. Each method demonstrates a different querying approach with EF Core.

## 1. LINQ with Include (ListAll)

**Method:** `ListAll()`

**Description:** Uses LINQ method syntax with eager loading of related data.

**Approach:**
- Uses `.Include()` to eagerly load the related Category navigation property
- Uses `.AsNoTracking()` for read-only queries (better performance)
- Converts to a list with `.ToList()`

**Pros:**
- Simple and readable
- Efficient eager loading prevents N+1 queries
- Type-safe

**Cons:**
- Returns fully tracked entities (unless using AsNoTracking)

**Code:**
```csharp
var items = _context.Products
                    .Include(c=> c.Category)
                    .AsNoTracking()
                    .ToList();
```

---

## 2. Stored Procedure (ListAllUsingStoredProc)

**Method:** `ListAllUsingStoredProc()`

**Description:** Executes a stored procedure from the database and maps results to Product entities.

**Approach:**
- Uses `.FromSqlRaw()` to execute a named stored procedure
- Explicitly loads the Category relationship with `.Load()`
- Checks if Category is null before loading to avoid unnecessary queries

**Pros:**
- Encapsulates complex database logic
- Good for performance-critical queries
- Database-side optimization

**Cons:**
- Requires stored procedure to exist in database
- Explicit relationship loading (potential N+1 if not careful)
- Less portable across database systems

**Code:**
```csharp
var items = _context.Products
                    .FromSqlRaw("spGetProducts")
                    .ToList();

// Explicit relationship loading
if (item.Category == null)
    _context.Entry(item).Reference(p => p.Category).Load();
```

---

## 3. Raw SQL Query (ListAllUsingRawSql)

**Method:** `ListAllUsingRawSql()`

**Description:** Executes raw SQL with complete control over the query.

**Approach:**
- Uses `.FromSqlRaw()` with custom SQL SELECT statement
- Includes all columns required for the Product entity
- Uses `.Include()` to load related Category data
- Uses `.AsNoTracking()` for read-only access

**Pros:**
- Full control over SQL generation
- Can optimize complex queries
- Clear what SQL is executed

**Cons:**
- Manual SQL writing (SQL injection risk if not parameterized)
- Less maintainable if schema changes
- Requires knowing all entity columns

**Code:**
```csharp
var items = _context.Products
                    .FromSqlRaw("SELECT p.RowId, p.Name, p.Price, p.CategoryId, p.Created, p.Modified " +
                               "FROM Products p")
                    .Include(p => p.Category)
                    .AsNoTracking()
                    .ToList();
```

---

## 4. LINQ Join Query (ListAllUsingLinq)

**Method:** `ListAllUsingLinq()`

**Description:** Uses LINQ query syntax with explicit join and anonymous type projection.

**Approach:**
- Uses LINQ `join` to combine Products and Categories
- Selects only the columns needed (anonymous type projection)
- Defers execution until `.ToList()` is called

**Pros:**
- Pure LINQ (database-agnostic)
- Only retrieves needed columns (smaller result set)
- Clear join logic
- Anonymous type reduces memory overhead

**Cons:**
- Returns anonymous types (cannot be reused as entities)
- Requires knowing the join condition
- Less flexibility for complex operations

**Code:**
```csharp
var items = from p in _context.Products
            join c in _context.Categories on p.CategoryId equals c.RowId
            select new
            {
                p.RowId,
                p.Name,
                p.Price,
                c.CategoryName
            };
```

---

## Comparison Table

| Method | Query Type | Eager Load | Entity Type | Performance | Use Case |
|--------|-----------|------------|-------------|-------------|----------|
| **ListAll** | LINQ Include | Yes | Product Entity | Good | Standard queries |
| **ListAllUsingStoredProc** | Stored Proc | Manual | Product Entity | Best | Complex/optimized queries |
| **ListAllUsingRawSql** | Raw SQL | Yes | Product Entity | Very Good | Custom SQL optimization |
| **ListAllUsingLinq** | LINQ Join | No | Anonymous | Good | Lightweight projections |

---

## Key Concepts

### AsNoTracking()
- Improves performance for read-only queries
- Entities are not tracked by the change tracker
- Use when you don't plan to modify the data

### Include()
- Eager loads navigation properties
- Prevents N+1 query problems
- Executes a single JOIN query

### FromSqlRaw()
- Executes raw SQL or stored procedures
- Returns mapped entities
- Cannot be composed with LINQ after calling it

### Anonymous Types
- Lightweight projection of specific columns
- Good for reducing memory footprint
- Cannot be used as entity types for updates

---

## Recommendations

- **Default:** Use `ListAll()` for most cases—simple, efficient, and readable
- **Optimization:** Use `ListAllUsingRawSql()` when you need specific SQL optimization
- **Complex Logic:** Use `ListAllUsingStoredProc()` for business logic stored in the database
- **Lightweight:** Use `ListAllUsingLinq()` when you need only specific columns
