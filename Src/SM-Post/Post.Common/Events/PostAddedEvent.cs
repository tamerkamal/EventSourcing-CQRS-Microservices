namespace Post.Common.Events;

using CQRS.Core.Events;

public class PostAddedEvent : BaseEvent
{
    public PostAddedEvent(string raisedBy, string text) : base(nameof(PostAddedEvent), raisedBy)
    {
        // assumed that author is same as the user who added the post (Raised the event)
        this.Author = raisedBy;
        this.Text = text;
    }

    public string Author { get; set; }
    public string Text { get; set; }
}
