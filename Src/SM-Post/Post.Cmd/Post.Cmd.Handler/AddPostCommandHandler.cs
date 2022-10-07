namespace Src.SMPost.Post.Cmd.Post.Cmd.Handler;

public class AddPostCommandHandler
{
    public void AddPost(Guid id, string author, string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException($"The value of {nameof(text)} can not be null or empty!");
        }

        RaiseEvent(new PostAddedEvent(author, text) { Id = id });
    }
}
