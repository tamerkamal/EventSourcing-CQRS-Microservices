namespace Post.Cmd.Domain.Aggregates;

using CQRS.Core.Domain;
using Post.Common.Events;

/// <summary>
/// Apply state Changes
/// </summary>
public partial class PostAggregate : AggregateRoot
{
    #region Post methods

    public void Apply(PostAddedEvent @event)
    {
        Id = @event.Id;
        _author = @event.RaisedBy;
        IsActive = true;
    }

    public void Apply(PostTextEditedEvent @event)
    {
        Id = @event.Id;
    }

    public void Apply(PostLikedEvent @event)
    {
        Id = @event.Id;
    }

    public void Apply(PostRemovedEvent @event)
    {
        Id = @event.Id;
        IsActive = false;
    }

    #endregion


    #region Comment methods

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

    #endregion
}

