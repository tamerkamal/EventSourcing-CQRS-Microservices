namespace Post.Query.Api.Queries.Handler;

using System.Collections.Generic;
using System.Threading.Tasks;
using Post.Common.Entities;
using Post.Query.Domain.Repositories;

public class QueryHandler : IQueryHandler
{
    private readonly IPostQueryRepository _postQueryRepository;

    public QueryHandler(IPostQueryRepository postQueryRepository)
    {
        _postQueryRepository = postQueryRepository;
    }

    public async Task<IEnumerable<PostEntity>> HandleAsync(GetAllPostsQuery query)
    {
        return await _postQueryRepository.GetAllAsync();
    }

    public async Task<IEnumerable<PostEntity>> HandleAsync(GetPostByIdQuery query)
    {
        var post = await _postQueryRepository.GetByIdAsync(query.Id);
        return new List<PostEntity> { post };
    }

    public async Task<IEnumerable<PostEntity>> HandleAsync(GetPostsHavingLikesQuery query)
    {
        return await _postQueryRepository.GetLikedAsync(query.MinimumNumOfLikes);
    }

    public async Task<IEnumerable<PostEntity>> HandleAsync(GetPostsByAuthorQuery query)
    {
        return await _postQueryRepository.GetByAuthorAsync(query.Author);
    }

    public async Task<IEnumerable<PostEntity>> HandleAsync(GetPostsHavingCommentsQuery query)
    {
        return await _postQueryRepository.GetHavingCommentsAsync();
    }
}
