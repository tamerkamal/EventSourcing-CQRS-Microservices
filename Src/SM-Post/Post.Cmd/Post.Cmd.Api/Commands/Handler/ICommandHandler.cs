namespace Post.Cmd.Api.RestoreAppDbComand.Handler;

using Post.Cmd.Api.Commands;
using Post.Cmd.Api.RestoreAppDbComand;

public interface ICommandHandler
{
    #region  Handling Post commands

    Task HandleAsync(AddPostCommand command);
    Task HandleAsync(EditPostTextCommand command);
    Task HandleAsync(LikePostCommand command);
    Task HandleAsync(RemovePostCommand command);

    #endregion

    #region  Handling Comment commands

    Task HandleAsync(AddCommentCommand command);
    Task HandleAsync(EditCommentCommand command);
    Task HandleAsync(RemoveCommentCommand command);

    #endregion

    #region Handling App. Database commands

    Task HandleAsync(RestoreAppDbCommand command);

    #endregion
}
