namespace Post.Query.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Post.Common.DbContexts;
using Post.Common.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Domain.Repositories.Base;
using Post.Query.Infrastructure.DataAccess;

public class CommentQueryRepository : ICommentQueryRepository //BaseQueryRepository<CommentEntity>
{
    private readonly DatabaseContextFactory _contextFactory;

    public CommentQueryRepository(DatabaseContextFactory contextFactory) //: base(databaseContextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Comments.ToListAsync();
    }

    public async Task<CommentEntity> GetByIdAsync(Guid entityId)
    {
        using DatabaseContext context = _contextFactory.CreateDbContext();

        return await context.Comments.FindAsync(entityId);
    }
}
