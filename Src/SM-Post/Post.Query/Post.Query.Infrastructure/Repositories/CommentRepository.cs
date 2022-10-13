using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories.Base;
using Post.Query.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories;

public class CommentRepository : BaseRepository<CommentEntity>
{
    private readonly DatabaseContextFactory<CommentEntity> _databaseContextFactory;

    public CommentRepository(DatabaseContextFactory<CommentEntity> databaseContextFactory) : base(databaseContextFactory)
    {
        _databaseContextFactory = databaseContextFactory;
    }
}
