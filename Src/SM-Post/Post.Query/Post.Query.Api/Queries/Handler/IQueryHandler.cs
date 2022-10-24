namespace Post.Query.Domain.Handlers;

using Post.Common.Entities;
using Post.Query.Api.Queries;

public interface IQueryHandler
{
    Task<IEnumerable<PostEntity>> HandleAsync(GetAllPostsQuery query);
    Task<PostEntity> HandleAsync(GetPostByIdQuery query);
    Task<IEnumerable<PostEntity>> HandleAsync(GetPostsHavingLikesQuery query);
    Task<IEnumerable<PostEntity>> HandleAsync(GetPostsByAuthorQuery query);
    Task<IEnumerable<PostEntity>> HandleAsync(GetPostsHavingCommentsQuery query);
}
