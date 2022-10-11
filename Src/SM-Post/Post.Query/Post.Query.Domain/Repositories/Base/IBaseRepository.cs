namespace Post.Query.Domain.Repositories.Base;

using CQRS.Core.Domain;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid entityId);
    Task<T> GetByIdAsync(Guid entityId);
    Task<List<T>> GetAllAsync();
}
