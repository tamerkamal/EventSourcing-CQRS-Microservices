namespace Post.Common.Events;

using CQRS.Core.Events;

public class CommentAddedEvent : BaseEvent
{
    public CommentAddedEvent(string raisedBy, Guid commentId, string comment) : base(nameof(CommentAddedEvent), raisedBy)
    {
        this.CommentId = commentId;
        this.Comment = comment;
    }

    public Guid CommentId { get; set; }
    public string Comment { get; set; }
}
