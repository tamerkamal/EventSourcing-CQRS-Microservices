namespace Post.Cmd.Infrastructure.DataAccess;

using Microsoft.EntityFrameworkCore;
using Post.Common.Entities;

public class DatabaseCmdContext : DbContext
{
    public DatabaseCmdContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
