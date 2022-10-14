namespace Post.Query.Domain.Repositories.Base;

using CQRS.Core.Domain;

public interface IBaseQueryRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(Guid entityId);
    Task<IEnumerable<T>> GetAllAsync();
}
