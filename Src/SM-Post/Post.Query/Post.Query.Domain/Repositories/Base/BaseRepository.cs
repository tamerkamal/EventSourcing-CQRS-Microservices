using CQRS.Core.Domain;

namespace Post.Query.Domain.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    public async Task CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }
    public async Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
    public async Task DeleteAsync(Guid entityId)
    {
        throw new NotImplementedException();
    }
    public async Task<T> GetByIdAsync(Guid entityId)
    {
        throw new NotImplementedException();
    }
    public async Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
