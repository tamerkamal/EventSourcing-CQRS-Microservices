namespace Post.Query.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories.Base;
using Post.Query.Infrastructure.DataAccess;

public class PostRepository : BaseRepository<PostEntity>
{
    private readonly DatabaseContextFactory<PostEntity> _databaseContextFactory;

    public PostRepository(DatabaseContextFactory<PostEntity> databaseContextFactory) : base(databaseContextFactory)
    {
        _databaseContextFactory = databaseContextFactory;
    }

    public override async Task<IEnumerable<PostEntity>> GetAllAsync()
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetByAuthorAsync(string author)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Author == author).ToListAsync();
            return entities;
        }
    }

    public override async Task<PostEntity> GetByIdAsNoTrackingAsync(Guid entityId)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var entity = await dbContext.Posts.AsNoTracking().Include(p => p.Comments).FirstOrDefaultAsync();
            return entity;
        }
    }

    public override async Task<PostEntity> GetByIdAsync(Guid entityId)
    {

        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var entity = await dbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync();
            return entity;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetHavingCommentsAsync()
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Comments.Any()).Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }

    public async Task<IEnumerable<PostEntity>> GetLikedAsync(int minimumLikes)
    {
        using (DatabaseContext dbContext = _databaseContextFactory.CreateDbContext())
        {
            var entities = await dbContext.Posts.AsNoTracking().Where(p => p.Likes >= minimumLikes).Include(p => p.Comments).ToListAsync();
            return entities;
        }
    }
}
