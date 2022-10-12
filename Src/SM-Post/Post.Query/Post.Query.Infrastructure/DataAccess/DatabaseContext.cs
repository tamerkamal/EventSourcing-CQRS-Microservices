namespace Post.Query.Infrastructure.DataAccess;

using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
