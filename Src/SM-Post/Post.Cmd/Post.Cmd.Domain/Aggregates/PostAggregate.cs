namespace Post.Cmd.Domain.Aggregates;

using CQRS.Core.Domain;
using Post.Common.Events;

public class PostAggregate : AggregateRoot
{
    #region State Propeties

    public bool IsActive { get; set; }
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

    #endregion

    #region Apply state Changes

    public void Apply(PostAddedEvent @event)
    {
        _id = @event.Id;
        IsActive = true;
        _author = @event.Author;
    }

    public void Apply(PostTextEditedEvent @event)
    {
        _id = @event.Id;
    }

    public void Apply(PostLikedEvent @event)
    {
        _id = @event.Id;
    }

    public void Apply(CommentAddedEvent @event)
    {
        _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.RaisedBy));
    }

    public void Apply(CommentEditedEvent @event)
    {
        _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.RaisedBy);
    }

    public void Apply(CommentRemovedEvent @event)
    {
        _comments.Remove(@event.CommentId);
    }

    public void Apply(PostRemovedEvent @event)
    {
        _id = @event.Id;
        IsActive = false;
    }

    #endregion
}
