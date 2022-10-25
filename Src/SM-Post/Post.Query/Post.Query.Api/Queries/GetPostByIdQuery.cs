namespace Post.Query.Api.Queries;

using CQRS.Core.Queries;

public class GetPostByIdQuery : BaseQuery
{
    public GetPostByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
