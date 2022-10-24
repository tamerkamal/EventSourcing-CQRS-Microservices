namespace Post.Query.Api.Queries;

using CQRS.Core.Queries;

public class GetPostsByAuthorQuery : BaseQuery
{
    public string Author { get; set; }
}
