namespace Post.Query.Domain.Repositories;

using Post.Common.Entities;
using Post.Query.Domain.Repositories.Base;

public interface IPostQueryRepository : IBaseQueryRepository<PostEntity>
{
    Task<List<PostEntity>> GetByAuthorAsync(string author);
    Task<List<PostEntity>> GetLikedAsync(int minimumLikes);
    Task<List<PostEntity>> GetHavingCommentsAsync();
}
