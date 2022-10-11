namespace Post.Query.Domain.Repositories;

using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories.Base;

public interface IPostRepository : IBaseRepository<PostEntity>
{
    // Task CreateAsync(PostEntity post);
    // Task UpdateAsync(PostEntity post);
    // Task DeleteAsync(Guid postId);
    // Task<PostEntity> GetByIdAsync(Guid postId);
    // Task<List<PostEntity>> GetAllAsync();
    Task<List<PostEntity>> GetByAuthorAsync(string author);
    Task<List<PostEntity>> GetLikedAsync(int minimumLikes);
    Task<List<PostEntity>> GetHavingCommentsAsync();

}
