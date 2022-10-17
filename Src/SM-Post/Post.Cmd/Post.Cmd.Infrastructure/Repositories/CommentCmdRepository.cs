namespace Post.Cmd.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Repositories;
using Post.Cmd.Infrastructure.DataAccess;
using Post.Cmd.Infrastructure.Repositories.Base;
using Post.Common.DbContexts;
using Post.Common.Entities;

public class CommentCmdRepository : ICommentCmdRepository //: BaseCmdRepository<CommentEntity>
{
    private readonly DatabaseContextFactory _contextFactory;

    public CommentCmdRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<CommentEntity> GetByIdAsync(Guid commentId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);
    }

    public async Task UpdateAsync(CommentEntity comment)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Comments.Update(comment);

        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        var comment = await GetByIdAsync(commentId);

        if (comment == null) return;

        context.Comments.Remove(comment);
        _ = await context.SaveChangesAsync();
    }

    public async Task CreateAsync(CommentEntity comment)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Comments.Add(comment);

        _ = await context.SaveChangesAsync();
    }

    // private readonly DatabaseCmdContextFactory<CommentEntity> _databaseCmdContextFactory;

    // public CommentCmdRepository(DatabaseCmdContextFactory<CommentEntity> databaseCmdContextFactory) : base(databaseCmdContextFactory)
    // {
    //     _databaseCmdContextFactory = databaseCmdContextFactory;
    // }
}
