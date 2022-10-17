namespace Post.Query.Domain.Repositories.Base;

using CQRS.Core.Domain;

public interface IBaseQueryRepository<Entity> where Entity : BaseEntity
{
    Task<Entity> GetByIdAsync(Guid entityId);
    Task<IEnumerable<Entity>> GetAllAsync();
}
