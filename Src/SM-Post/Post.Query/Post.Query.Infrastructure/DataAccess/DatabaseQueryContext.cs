namespace Post.Query.Infrastructure.DataAccess;

using Microsoft.EntityFrameworkCore;
using Post.Common.Entities;

public class DatabaseQueryContext : DbContext
{
    public DatabaseQueryContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
