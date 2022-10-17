namespace Post.Query.Domain.Repositories;

using Post.Common.Entities;
using Post.Query.Domain.Repositories.Base;

public interface IPostQueryRepository// : IBaseQueryRepository<PostEntity>
{
    Task<PostEntity> GetByIdAsync(Guid postId);
    Task<List<PostEntity>> GetAllAsync();
    Task<IEnumerable<PostEntity>> GetByAuthorAsync(string author);
    Task<IEnumerable<PostEntity>> GetLikedAsync(int minimumLikes);
    Task<IEnumerable<PostEntity>> GetHavingCommentsAsync();
}
