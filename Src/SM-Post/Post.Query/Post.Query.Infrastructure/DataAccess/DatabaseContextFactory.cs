namespace Post.Query.Infrastructure.DataAccess;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;

public class DatabaseContextFactory<T> where T : BaseEntity
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }

    public DatabaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseContext> dbContextOptions = new();
        _configureDbContext(dbContextOptions);

        return new DatabaseContext(dbContextOptions.Options);
    }

    public DbSet<T> CreateDbSet()
    {
        return CreateDbContext().Set<T>();
    }
}
