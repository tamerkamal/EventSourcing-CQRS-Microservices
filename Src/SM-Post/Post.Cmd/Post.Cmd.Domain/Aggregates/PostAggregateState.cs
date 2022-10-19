using CQRS.Core.Domain;

namespace Post.Cmd.Domain.Aggregates;

/// <summary>
/// Post Aggregate State Properties
/// </summary>
public partial class PostAggregate : AggregateRoot
{
    public bool IsActive { get; set; }
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();
}
