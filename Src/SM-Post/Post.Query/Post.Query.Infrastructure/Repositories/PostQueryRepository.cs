namespace Post.Query.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Post.Common.DbContexts;
using Post.Common.Entities;
using Post.Query.Domain.Repositories;

public class PostQueryRepository : IPostQueryRepository //BaseQueryRepository<PostEntity>
{
    private readonly DatabaseContextFactory _contextFactory;
    public PostQueryRepository(DatabaseContextFactory contextFactory) // : base(databaseQueryContextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<PostEntity>> GetAllAsync()
    {
        using (DatabaseContext dbContext = _contextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetByAuthorAsync(string author)
    {
        using (DatabaseContext dbContext = _contextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Author == author).ToListAsync();
            return entities;
        }
    }

    public async Task<PostEntity> GetByIdAsync(Guid entityId)
    {

        using (DatabaseContext dbContext = _contextFactory.CreateDbContext())
        {
            var entity = await dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync();
            return entity;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetHavingCommentsAsync()
    {
        using (DatabaseContext dbContext = _contextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Comments.Any()).Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetLikedAsync(int minimumLikes)
    {
        using (DatabaseContext dbContext = _contextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Likes >= minimumLikes).Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }
}
