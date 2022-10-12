namespace Post.Query.Infrastructure.DataAccess;

using Microsoft.EntityFrameworkCore;

public class DatabaseContextFactory
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
}
