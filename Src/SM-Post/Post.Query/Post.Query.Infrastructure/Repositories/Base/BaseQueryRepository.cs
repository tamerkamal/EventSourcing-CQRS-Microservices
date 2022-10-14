namespace Post.Query.Domain.Repositories.Base;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Query.Infrastructure.DataAccess;

public abstract class BaseQueryRepository<T> : IBaseQueryRepository<T> where T : BaseEntity
{
    private readonly DatabaseQueryContextFactory<T> _databaseContextFactory;
    private DbSet<T> _entities;

    public BaseQueryRepository(DatabaseQueryContextFactory<T> databaseContextFactory)
    {
        _databaseContextFactory = databaseContextFactory;
        _entities = _databaseContextFactory.CreateDbSet();
    }

    public virtual async Task<T> GetByIdAsync(Guid entityId)
    {
        using (DatabaseQueryContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var result = await _entities.FindAsync(entityId);
            return result;
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using (DatabaseQueryContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var results = await _entities.AsNoTracking().ToListAsync();
            return results;
        }
    }
}

