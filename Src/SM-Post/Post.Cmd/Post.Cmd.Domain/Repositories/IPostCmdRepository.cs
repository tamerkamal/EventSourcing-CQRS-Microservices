namespace Post.Cmd.Domain.Repositories;

using Post.Cmd.Domain.Repositories.Base;
using Post.Common.Entities;

public interface IPostCmdRepository //: IBaseCmdRepository<PostEntity>
{
    Task CreateAsync(PostEntity post);
    Task UpdateAsync(PostEntity post);
    Task DeleteAsync(Guid postId);
    Task<PostEntity> GetByIdAsync(Guid postId);
}
