namespace Post.Cmd.Domain.Aggregates;

using CQRS.Core.Domain;

public class PostAggregate : AggregateRoot
{
    private bool _isActive;
    private string _author;

    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();


    public bool IsActive
    {
        get => _isActive; set => _isActive = value;
    }

    public PostAggregate()
    {

    }

    public PostAggregate(Guid id, string author, string text)
    {
        // RaiseEvent(new PostAddedEvent()){

        // }
    }
}
