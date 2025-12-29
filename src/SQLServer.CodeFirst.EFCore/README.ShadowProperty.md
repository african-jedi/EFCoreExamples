# EF Core Shadow Property
A shadow property is a property not in the entity but tracked by EF Core. Shadow property can be added for auditing.

## Adding Shadow Property
1. Override the "OnModelCreating" method
2. "modelBuilder.Entity<Post>().Property<string>("User");"
3. Override "SaveChanges" method to save the User shadow property

```c#
 public int SaveChanges(string userName)
    {
        foreach(var entry in ChangeTracker.Entries())
        {
            if(entry.State == EntityState.Added)
              entry.Property("User").CurrentValue = userName;
            if(entry.State == EntityState.Modified)
              entry.Property("User").CurrentValue = userName;
        }
        return base.SaveChanges();
    }
```
