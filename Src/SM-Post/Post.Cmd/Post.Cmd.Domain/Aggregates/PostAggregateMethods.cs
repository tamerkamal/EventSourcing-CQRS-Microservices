namespace Post.Cmd.Domain.Aggregates;

using CQRS.Core.Domain;
using Post.Common.Events;

public partial class PostAggregate : AggregateRoot
{
    #region Post methods   

    public void AddPost(Guid id, string author, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException($"The value of {nameof(text)} can not be null or empty!");
        }

        RaiseEvent(new PostAddedEvent(author, text) { Id = id });
    }

    public void EditPostText(string text, string raisedBy)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not edit text of a removed post!");
        }

        if (!_author.Equals(raisedBy, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can edit a post added by someone else!");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException($"The value of {nameof(text)} can not be null or empty!");
        }

        RaiseEvent(new PostTextEditedEvent(_author, text) { Id = Id });
    }

    public void LikePost(string raisedBy)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not like a removed post!");
        }

        RaiseEvent(new PostLikedEvent(raisedBy) { Id = Id });
    }

    public void RemovePost(string raisedBy)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Posta already removed!");
        }

        if (!_author.Equals(raisedBy, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new($"You can remove a post added by someone else!");
        }

        RaiseEvent(new PostRemovedEvent(raisedBy) { Id = Id });
    }

    #endregion

    #region Comment methods

    public void AddComment(Guid commentId, string comment, string commenter)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not comment on a removed post!");
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The value of {nameof(comment)} can not be null or empty!");
        }

        RaiseEvent(new CommentAddedEvent(commenter, commentId, comment) { Id = Id });
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

        RaiseEvent(new CommentEditedEvent(commenter, commentId, comment) { Id = Id });
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

        RaiseEvent(new CommentRemovedEvent(commenter, commentId) { Id = Id });
    }

    #endregion 

}
