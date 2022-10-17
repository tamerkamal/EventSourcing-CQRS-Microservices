namespace Post.Cmd.Domain.Repositories;

using Post.Cmd.Domain.Repositories.Base;
using Post.Common.Entities;

public interface ICommentCmdRepository //: IBaseCmdRepository<CommentEntity>
{
    Task CreateAsync(CommentEntity comment);
    Task<CommentEntity> GetByIdAsync(Guid commentId);
    Task UpdateAsync(CommentEntity comment);
    Task DeleteAsync(Guid commentId);
}
