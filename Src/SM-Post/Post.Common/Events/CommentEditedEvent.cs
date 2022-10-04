namespace Post.Common.Events;

using CQRS.Core.Events;

public class CommentEditedEvent : BaseEvent
{
    public CommentEditedEvent(Guid id, string raisedBy, Guid commentId, string comment) : base(nameof(CommentEditedEvent), raisedBy)
    {
        this.CommentId = commentId;
        this.Comment = comment;
    }

    public Guid CommentId { get; set; }
    public string Comment { get; set; }
}
