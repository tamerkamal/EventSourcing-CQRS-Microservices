namespace Post.Cmd.Infrastructure.DataAccess;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;

public class DatabaseCmdContextFactory<T> where T : BaseEntity
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;

    public DatabaseCmdContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }

    public DatabaseCmdContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseCmdContext> dbContextOptions = new();
        _configureDbContext(dbContextOptions);

        return new DatabaseCmdContext(dbContextOptions.Options);
    }

    public DbSet<T> CreateDbSet()
    {
        return CreateDbContext().Set<T>();
    }
}
