namespace Src.SMPost.Post.Cmd.Post.Cmd.Handler;

public class LikePostCommandHandler
{
    public void LikePost(string raisedBy)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("Can not like a removed post!");
        }

        RaiseEvent(new PostLikedEvent(raisedBy) { Id = _id });
    }
}
