namespace Post.Query.Infrastructure.DataAccess;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;

public class DatabaseQueryContextFactory<T> where T : BaseEntity
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public DatabaseQueryContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }

    public DatabaseQueryContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseQueryContext> dbContextOptions = new();
        _configureDbContext(dbContextOptions);

        return new DatabaseQueryContext(dbContextOptions.Options);
    }

    public DbSet<T> CreateDbSet()
    {
        return CreateDbContext().Set<T>();
    }
}
