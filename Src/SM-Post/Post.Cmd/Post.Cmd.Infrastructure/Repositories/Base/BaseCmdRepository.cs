namespace Post.Cmd.Infrastructure.Repositories.Base;

using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Repositories.Base;
using Post.Cmd.Infrastructure.DataAccess;

public class BaseCmdRepository<Entity> : IBaseCmdRepository<Entity> where Entity : BaseEntity
{
    private readonly DatabaseCmdContextFactory<Entity> _databaseCmdContextFactory;
    private DbSet<Entity> _entities;

    public BaseCmdRepository(DatabaseCmdContextFactory<Entity> databaseCmdContextFactory)
    {
        _databaseCmdContextFactory = databaseCmdContextFactory;
        _entities = _databaseCmdContextFactory.CreateDbSet();
    }

    public virtual async Task CreateAsync(Entity entity)
    {
        using (DatabaseCmdContext dbContext = _databaseCmdContextFactory.CreateDbContext())
        {
            _entities.Add(entity);

            await dbContext.SaveChangesAsync();
        }
    }
    public virtual async Task UpdateAsync(Entity entity)
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

    public async Task<Entity> GetByIdAsync(Guid entityId)
    {
        var entity = await _entities.FindAsync(entityId);

        return entity;
    }
}
