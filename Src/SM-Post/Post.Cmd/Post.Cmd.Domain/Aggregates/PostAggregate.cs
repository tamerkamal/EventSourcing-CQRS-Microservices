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

    public void EditPostText(string text)
    {
        if (_isActive)
        {
            throw new InvalidOperationException("Can not edit text of a removed post!");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException($"The value of {nameof(text)} can not be null or empty!");
        }

        RaiseEvent(new PostTextEditedEvent(
           _author, text
         ));
    }

    public void Apply(PostTextEditedEvent @event)
    {
    }

    public void LikePost()
    {
        if (_isActive)
        {
            throw new InvalidOperationException("Can not like a removed post!");
        }

        RaiseEvent(new PostLikedEvent(
           _author
         ));
    }

    public void Apply(PostLikedEvent @event)
    {

    }

    public void AddComment(string comment, string commenter)
    {
        if (_isActive)
        {
            throw new InvalidOperationException("Can not comment on a removed post!");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The value of {nameof(comment)} can not be null or empty!");
        }


        RaiseEvent(new CommentAddedEvent(
           commenter, comment
         ));
    }

    public void Apply(CommentAddedEvent @event)
    {
        _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.RaisedBy));
    }

    public void EditComment(Guid commentId, string comment, string commenter)
    {
        if (_isActive)
        {
            throw new InvalidOperationException("Can not edit comment on a removed post!");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The value of {nameof(comment)} can not be null or empty!");
        }

        if (!_comments[commentId].Item2.Equals(commenter, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can edit a comment added by another user!");
        }

        RaiseEvent(new CommentEditedEvent(
            commenter, commentId, comment
         ));
    }

    public void Apply(CommentEditedEvent @event)
    {
        _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.RaisedBy);
    }

    public void RemoveComment(Guid commentId, string commenter)
    {
        if (_isActive)
        {
            throw new InvalidOperationException("Can not remove comment on a removed post!");
        }

        if (!_comments[commentId].Item2.Equals(commenter, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can remove a comment added by someone else!");
        }

        RaiseEvent(new CommentRemovedEvent(
            commenter, commentId
         ));
    }

    public void Apply(CommentRemovedEvent @event)
    {
        _comments.Remove(@event.CommentId);
    }

    public void RemovePost(string author)
    {
        if (_isActive)
        {
            throw new InvalidOperationException("Posta already removed!");
        }

        if (!_author.Equals(author, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can remove a post added by someone else!");
        }

        RaiseEvent(new PostRemovedEvent(
             author
         )
        { Id = _id });
    }

    public void Apply(PostRemovedEvent @event)
    {
        _id = @event.Id;
        _isActive = false;
    }

    #endregion
}
