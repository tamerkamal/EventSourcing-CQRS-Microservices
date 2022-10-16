namespace Post.Cmd.Infrastructure.Repositories;

using Post.Cmd.Infrastructure.DataAccess;
using Post.Cmd.Infrastructure.Repositories.Base;
using Post.Common.Entities;

public class CommentCmdRepository : BaseCmdRepository<CommentEntity>
{
    private readonly DatabaseCmdContextFactory<CommentEntity> _databaseCmdContextFactory;

    public CommentCmdRepository(DatabaseCmdContextFactory<CommentEntity> databaseCmdContextFactory) : base(databaseCmdContextFactory)
    {
        _databaseCmdContextFactory = databaseCmdContextFactory;
    }
}
