namespace Post.Cmd.Domain.Aggregates;

using Post.Common.Events;
public partial class PostAggregate
{
    #region Private methods (Apply state Changes)

    private void Apply(PostTextEditedEvent @event)
    {
        Id = @event.Id;
    }

    private void Apply(PostLikedEvent @event)
    {
        Id = @event.Id;
    }

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

    private void Apply(PostRemovedEvent @event)
    {
        Id = @event.Id;
        IsActive = false;
    }

    #endregion
}

