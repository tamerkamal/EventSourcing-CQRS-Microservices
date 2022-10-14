namespace Post.Cmd.Infrastructure.Repositories;

using Post.Cmd.Domain.Entities;
using Post.Cmd.Infrastructure.DataAccess;
using Post.Cmd.Infrastructure.Repositories.Base;

public class PostCmdRepository : BaseCmdRepository<PostEntity>
{
    private readonly DatabaseCmdContextFactory<PostEntity> _databaseCmdContextFactory;

    public PostCmdRepository(DatabaseCmdContextFactory<PostEntity> databaseCmdContextFactory) : base(databaseCmdContextFactory)
    {
        _databaseCmdContextFactory = databaseCmdContextFactory;
    }
}
