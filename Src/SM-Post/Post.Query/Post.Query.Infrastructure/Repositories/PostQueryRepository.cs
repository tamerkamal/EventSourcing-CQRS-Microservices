namespace Post.Query.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Post.Common.Entities;
using Post.Query.Domain.Repositories.Base;
using Post.Query.Infrastructure.DataAccess;

public class PostQueryRepository : BaseQueryRepository<PostEntity>
{
    private readonly DatabaseQueryContextFactory<PostEntity> _databaseQueryContextFactory;
    public PostQueryRepository(DatabaseQueryContextFactory<PostEntity> databaseQueryContextFactory) : base(databaseQueryContextFactory)
    {
        _databaseQueryContextFactory = databaseQueryContextFactory;
    }

    public override async Task<IEnumerable<PostEntity>> GetAllAsync()
    {
        using (DatabaseQueryContext dbContext = _databaseQueryContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetByAuthorAsync(string author)
    {
        using (DatabaseQueryContext dbContext = _databaseQueryContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Author == author).ToListAsync();
            return entities;
        }
    }

    public override async Task<PostEntity> GetByIdAsync(Guid entityId)
    {

        using (DatabaseQueryContext dbContext = _databaseQueryContextFactory.CreateDbContext())
        {
            var entity = await dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync();
            return entity;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetHavingCommentsAsync()
    {
        using (DatabaseQueryContext dbContext = _databaseQueryContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Comments.Any()).Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetLikedAsync(int minimumLikes)
    {
        using (DatabaseQueryContext dbContext = _databaseQueryContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Likes >= minimumLikes).Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }
}
