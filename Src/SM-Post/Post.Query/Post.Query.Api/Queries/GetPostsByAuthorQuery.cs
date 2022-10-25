namespace Post.Query.Api.Queries;

using CQRS.Core.Queries;

public class GetPostsByAuthorQuery : BaseQuery
{
    public GetPostsByAuthorQuery(string author)
    {
        Author = author;
    }

    public string Author { get; set; }
}
