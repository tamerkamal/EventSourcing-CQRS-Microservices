namespace Post.Cmd.Domain.Aggregates;

using CQRS.Core.Domain;
using Post.Common.Events;

public class PostAggregate : AggregateRoot
{
    #region Properties

    private bool _isActive;
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();
    public bool IsActive { get; set; }

    #endregion

    #region Constructors

    public PostAggregate()
    {

    }

    public PostAggregate(Guid id, string author, string text)
    {
        RaiseEvent(new PostAddedEvent(author, text));
    }

    #endregion

    #region Public methods

    public void Apply(PostAddedEvent @event)
    {
        _id = @event.Id;
        _isActive = true;
        _author = @event.Author;
    }

    #endregion
}
