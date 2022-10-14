namespace Post.Query.Domain.Repositories;

using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories.Base;

public interface IPostQueryRepository : IBaseQueryRepository<PostEntity>
{
    Task<IEnumerable<PostEntity>> GetByAuthorAsync(string author);
    Task<IEnumerable<PostEntity>> GetLikedAsync(int minimumLikes);
    Task<IEnumerable<PostEntity>> GetHavingCommentsAsync();
}
