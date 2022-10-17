namespace Post.Cmd.Domain.Repositories.Base;

using CQRS.Core.Domain;

public interface IBaseCmdRepository<Entity> where Entity : BaseEntity
{
    Task CreateAsync(Entity entity);
    Task UpdateAsync(Entity entity);
    Task DeleteAsync(Guid entityId);
    Task<Entity> GetByIdAsync(Guid entityId);
}
