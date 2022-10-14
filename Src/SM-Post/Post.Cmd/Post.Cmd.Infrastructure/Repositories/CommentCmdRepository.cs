namespace Post.Cmd.Infrastructure.Repositories;

using Post.Cmd.Domain.Entities;
using Post.Cmd.Infrastructure.DataAccess;
using Post.Cmd.Infrastructure.Repositories.Base;

public class CommentCmdRepository : BaseCmdRepository<CommentEntity>
{
    private readonly DatabaseCmdContextFactory<CommentEntity> _databaseCmdContextFactory;

    public CommentCmdRepository(DatabaseCmdContextFactory<CommentEntity> databaseCmdContextFactory) : base(databaseCmdContextFactory)
    {
        _databaseCmdContextFactory = databaseCmdContextFactory;
    }
}
