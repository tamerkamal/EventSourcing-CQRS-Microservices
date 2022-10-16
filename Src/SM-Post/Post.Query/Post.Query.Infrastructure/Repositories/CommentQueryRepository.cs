namespace Post.Query.Infrastructure.Repositories;

using Post.Common.Entities;
using Post.Query.Domain.Repositories.Base;
using Post.Query.Infrastructure.DataAccess;

public class CommentQueryRepository : BaseQueryRepository<CommentEntity>
{
    public CommentQueryRepository(DatabaseQueryContextFactory<CommentEntity> databaseContextFactory) : base(databaseContextFactory)
    {

    }
}
