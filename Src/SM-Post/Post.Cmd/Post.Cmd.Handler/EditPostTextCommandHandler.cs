namespace Src.SMPost.Post.Cmd.Post.Cmd.Handler;
public class EditPostTextCommandHandler
{
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
}
