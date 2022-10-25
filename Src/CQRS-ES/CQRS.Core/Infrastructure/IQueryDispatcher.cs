namespace CQRS.Core.Infrastructure;

using CQRS.Core.Queries;

public interface IQueryDispatcher<TEntity>
{
    void RegisterHandler<TQuery>(Func<TQuery, Task<IEnumerable<TEntity>>> handler) where TQuery : BaseQuery;

    Task<IEnumerable<TEntity>> SendAsync(BaseQuery query);
}
