namespace Post.Query.Api.Queries;

using CQRS.Core.Queries;

public class GetPostsHavingLikesQuery : BaseQuery
{
    public int MinimumNumOfLikes { get; set; }
}
