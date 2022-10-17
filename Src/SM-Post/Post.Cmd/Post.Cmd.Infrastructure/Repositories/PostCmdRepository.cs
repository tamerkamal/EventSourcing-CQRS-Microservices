namespace Post.Cmd.Infrastructure.Repositories;

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Repositories;
using Post.Common.DbContexts;
using Post.Common.Entities;

public class PostCmdRepository : IPostCmdRepository//: BaseCmdRepository<PostEntity>
{
    private readonly DatabaseContextFactory _contextFactory;

    public PostCmdRepository(DatabaseContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<PostEntity> GetByIdAsync(Guid postId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
    }

    public async Task UpdateAsync(PostEntity post)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Posts.Update(post);

        _ = await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid postId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        var post = await GetByIdAsync(postId);

        if (post == null) return;

        context.Posts.Remove(post);
        _ = await context.SaveChangesAsync();
    }

    public async Task CreateAsync(PostEntity post)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();
        context.Posts.Add(post);

        _ = await context.SaveChangesAsync();
    }

    // private readonly DatabaseCmdContextFactory<PostEntity> _databaseCmdContextFactory;

    // public PostCmdRepository(DatabaseCmdContextFactory<PostEntity> databaseCmdContextFactory) : base(databaseCmdContextFactory)
    // {
    //     _databaseCmdContextFactory = databaseCmdContextFactory;
    // }  
}
