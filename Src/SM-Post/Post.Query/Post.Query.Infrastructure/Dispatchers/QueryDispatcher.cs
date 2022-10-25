namespace Post.Query.Infrastructure.Dispatchers;

using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Post.Common.Entities;

public class QueryDispatcher : IQueryDispatcher<PostEntity>
{

    private readonly Dictionary<Type, Func<BaseQuery, Task<IEnumerable<PostEntity>>>> _queryHandlers = new();

    public void RegisterHandler<TQuery>(Func<TQuery, Task<IEnumerable<PostEntity>>> handler) where TQuery : BaseQuery
    {
        if (_queryHandlers.ContainsKey(typeof(TQuery)))
        {
            throw new IndexOutOfRangeException("You can not register the same handler twice");
        }

        _queryHandlers.Add(typeof(TQuery), q => handler((TQuery)q));
    }

    public async Task<IEnumerable<PostEntity>> SendAsync(BaseQuery query)
    {
        if (!_queryHandlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<IEnumerable<PostEntity>>> handler))
        {
            return await handler(query);
        }

        throw new ArgumentNullException($"{nameof(handler)}, No query handler was registered!");
    }
}
