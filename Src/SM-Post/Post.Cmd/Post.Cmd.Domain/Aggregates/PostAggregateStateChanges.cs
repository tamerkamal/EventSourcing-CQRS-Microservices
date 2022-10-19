namespace Post.Cmd.Domain.Aggregates;

using Post.Common.Events;

/// <summary>
/// Apply state Changes
/// </summary>
public partial class PostAggregate
{
    #region Post methods

    private void Apply(PostAddedEvent @event)
    {
        Id = @event.Id;
        _author = @event.RaisedBy;
        IsActive = true;
    }

    private void Apply(PostTextEditedEvent @event)
    {
        Id = @event.Id;
    }

    private void Apply(PostLikedEvent @event)
    {
        Id = @event.Id;
    }

    private void Apply(PostRemovedEvent @event)
    {
        Id = @event.Id;
        IsActive = false;
    }

    #endregion


    #region Comment methods

    private void Apply(CommentAddedEvent @event)
    {
        _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.RaisedBy));
    }

    private void Apply(CommentEditedEvent @event)
    {
        _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.RaisedBy);
    }

    private void Apply(CommentRemovedEvent @event)
    {
        _comments.Remove(@event.CommentId);
    }

    #endregion
}

