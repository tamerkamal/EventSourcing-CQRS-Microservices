namespace Post.Cmd.Infrastructure.Repositories.Base;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Repositories.Base;
using Post.Cmd.Infrastructure.DataAccess;

public class BaseCmdRepository<T> : IBaseCmdRepository<T> where T : BaseEntity
{
    private readonly DatabaseCmdContextFactory<T> _databaseCmdContextFactory;
    private DbSet<T> _entities;

    public BaseCmdRepository(DatabaseCmdContextFactory<T> databaseCmdContextFactory)
    {
        _databaseCmdContextFactory = databaseCmdContextFactory;
        _entities = _databaseCmdContextFactory.CreateDbSet();
    }

    public virtual async Task CreateAsync(T entity)
    {
        using (DatabaseCmdContext dbContext = _databaseCmdContextFactory.CreateDbContext())
        {
            _entities.Add(entity);

            await dbContext.SaveChangesAsync();
        }
    }
    public virtual async Task UpdateAsync(T entity)
    {
        using (DatabaseCmdContext dbContext = _databaseCmdContextFactory.CreateDbContext())
        {
            _entities.Update(entity);

            await dbContext.SaveChangesAsync();
        }
    }
    public virtual async Task DeleteAsync(Guid entityId)
    {
        using (DatabaseCmdContext dbContext = _databaseCmdContextFactory.CreateDbContext())
        {
            var entity = await _entities.FindAsync(entityId);

            if (entity is null) { return; }

            _entities.Remove(entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
