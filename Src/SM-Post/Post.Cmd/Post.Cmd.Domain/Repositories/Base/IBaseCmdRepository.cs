namespace Post.Cmd.Domain.Repositories.Base;

using CQRS.Core.Domain;

public interface IBaseCmdRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid entityId);
}
