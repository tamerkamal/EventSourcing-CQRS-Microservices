namespace Post.Cmd.Domain.Aggregates;

using CQRS.Core.Domain;
using Post.Common.Events;

public class PostAggregate : AggregateRoot
{
    #region Properties

    public bool IsActive { get; set; }
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

    #endregion

    #region Constructors

    public PostAggregate()
    {

    }

    public PostAggregate(Guid id, string author, string text)
    {
        RaiseEvent(new PostAddedEvent(author, text) { Id = id });
    }

    #endregion

    #region Public methods

    public void Apply(PostAddedEvent @event)
    {
        _id = @event.Id;
        IsActive = true;
        _author = @event.Author;
    }

    public void EditPostText(string text)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not edit text of a removed post!");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException($"The value of {nameof(text)} can not be null or empty!");
        }

        RaiseEvent(new PostTextEditedEvent(_author, text) { Id = _id });
    }

    public void Apply(PostTextEditedEvent @event)
    {
        _id = @event.Id;
    }

    public void LikePost(string raisedBy)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not like a removed post!");
        }

        RaiseEvent(new PostLikedEvent(raisedBy) { Id = _id });
    }

    public void Apply(PostLikedEvent @event)
    {
        _id = @event.Id;
    }

    public void AddComment(string comment, string commenter)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not comment on a removed post!");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The value of {nameof(comment)} can not be null or empty!");
        }

        RaiseEvent(new CommentAddedEvent(commenter, Guid.NewGuid(), comment) { Id = _id });
    }

    public void Apply(CommentAddedEvent @event)
    {
        _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.RaisedBy));
    }

    public void EditComment(Guid commentId, string comment, string commenter)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not edit comment on a removed post!");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The value of {nameof(comment)} can not be null or empty!");
        }

        if (!_comments[commentId].Item2.Equals(commenter, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can't edit a comment added by someone else!");
        }

        RaiseEvent(new CommentEditedEvent(commenter, commentId, comment) { Id = _id });
    }

    public void Apply(CommentEditedEvent @event)
    {
        _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.RaisedBy);
    }

    public void RemoveComment(Guid commentId, string commenter)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can't remove a comment on a removed post!");
        }

        if (!_comments[commentId].Item2.Equals(commenter, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can't remove a comment added by someone else!");
        }

        RaiseEvent(new CommentRemovedEvent(commenter, commentId) { Id = _id });
    }

    public void Apply(CommentRemovedEvent @event)
    {
        _comments.Remove(@event.CommentId);
    }

    public void RemovePost(string author)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Posta already removed!");
        }

        if (!_author.Equals(author, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can remove a post added by someone else!");
        }

        RaiseEvent(new PostRemovedEvent(author) { Id = _id });
    }

    public void Apply(PostRemovedEvent @event)
    {
        _id = @event.Id;
        IsActive = false;
    }

    #endregion
}
