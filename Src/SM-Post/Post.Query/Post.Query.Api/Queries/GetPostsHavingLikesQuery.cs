namespace Post.Query.Api.Queries;

using CQRS.Core.Queries;

public class GetPostsHavingLikesQuery : BaseQuery
{
    public GetPostsHavingLikesQuery(int minimumNumOfLikes)
    {
        MinimumNumOfLikes = minimumNumOfLikes;
    }

    public int MinimumNumOfLikes { get; set; }
}
