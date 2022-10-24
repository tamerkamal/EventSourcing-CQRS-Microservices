namespace Post.Query.Api.Queries;

using CQRS.Core.Queries;

public class GetPostByIdQuery : BaseQuery
{
    public Guid Id { get; set; }
}
