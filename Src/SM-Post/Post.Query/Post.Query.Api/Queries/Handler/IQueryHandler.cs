namespace Post.Query.Api.Queries.Handler;

using Post.Common.Entities;
using Post.Query.Api.Queries;

public interface IQueryHandler
{
    Task<List<PostEntity>> HandleAsync(GetAllPostsQuery query);
    Task<List<PostEntity>> HandleAsync(GetPostByIdQuery query);
    Task<List<PostEntity>> HandleAsync(GetPostsHavingLikesQuery query);
    Task<List<PostEntity>> HandleAsync(GetPostsByAuthorQuery query);
    Task<List<PostEntity>> HandleAsync(GetPostsHavingCommentsQuery query);
}
