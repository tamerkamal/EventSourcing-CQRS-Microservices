namespace Post.Common.Events;

using CQRS.Core.Events;

public class CommentRemovedEvent : BaseEvent
{
    public CommentRemovedEvent(string raisedBy, Guid commentId) : base(nameof(CommentRemovedEvent), raisedBy)
    {

    }
}
