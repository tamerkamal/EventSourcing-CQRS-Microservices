namespace Post.Query.Domain.Repositories.Base;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Query.Infrastructure.DataAccess;

[Obsolete]
public abstract class BaseQueryRepository<Entity> : IBaseQueryRepository<Entity> where Entity : BaseEntity
{
    private readonly DatabaseQueryContextFactory<Entity> _databaseContextFactory;
    private DbSet<Entity> _entities;

    public BaseQueryRepository(DatabaseQueryContextFactory<Entity> databaseContextFactory)
    {
        _databaseContextFactory = databaseContextFactory;
        _entities = _databaseContextFactory.CreateDbSet();
    }

    public virtual async Task<Entity> GetByIdAsync(Guid entityId)
    {
        using (DatabaseQueryContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var result = await _entities.FindAsync(entityId);
            return result;
        }
    }

    public virtual async Task<List<Entity>> GetAllAsync()
    {
        using (DatabaseQueryContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var results = await _entities.AsNoTracking().ToListAsync();
            return results;
        }
    }
}

