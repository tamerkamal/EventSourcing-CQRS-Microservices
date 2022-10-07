namespace Src.SMPost.Post.Cmd.Post.Cmd.Handler;

public class RemovePostCommandHandler
{
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
}
