namespace Post.Query.Domain.Repositories.Base;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Query.Infrastructure.DataAccess;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DatabaseContextFactory<T> _databaseContextFactory;
    private DbSet<T> _entities;

    public BaseRepository(DatabaseContextFactory<T> databaseContextFactory)
    {
        _databaseContextFactory = databaseContextFactory;
        _entities = _databaseContextFactory.CreateDbSet();
    }

    public async Task CreateAsync(T entity)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            _entities.Add(entity);

            await dbContext.SaveChangesAsync();
        }
    }
    public async Task UpdateAsync(T entity)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            _entities.Update(entity);

            await dbContext.SaveChangesAsync();
        }
    }
    public async Task DeleteAsync(Guid entityId)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var entity = await GetByIdAsync(entityId);

            if (entity is null) { return; }

            _entities.Remove(entity);

            await dbContext.SaveChangesAsync();
        }
    }
    public virtual async Task<T> GetByIdAsync(Guid entityId)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var results = await _entities.FirstOrDefaultAsync();
            return results;
        }
    }

    public virtual async Task<T> GetByIdAsNoTrackingAsync(Guid entityId)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var results = await _entities.AsNoTracking().FirstOrDefaultAsync();
            return results;
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var results = await _entities.AsNoTracking().ToListAsync();
            return results;
        }
    }
}
