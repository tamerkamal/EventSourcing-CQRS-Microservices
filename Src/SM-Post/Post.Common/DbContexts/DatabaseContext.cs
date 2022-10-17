namespace Post.Common.DbContexts;

using Microsoft.EntityFrameworkCore;
using Post.Common.Entities;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
