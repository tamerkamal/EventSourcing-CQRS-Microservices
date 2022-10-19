namespace Post.Cmd.Api.Commands.Handler;

using Post.Cmd.Api.Commands;

public interface ICommandHandler
{
    #region  Handling Post Commands

    Task HandleAsync(AddPostCommand command);
    Task HandleAsync(EditPostTextCommand command);
    Task HandleAsync(LikePostCommand command);
    Task HandleAsync(RemovePostCommand command);

    #endregion

    #region  Handling Comment Commands

    Task HandleAsync(AddCommentCommand command);
    Task HandleAsync(EditCommentCommand command);
    Task HandleAsync(RemoveCommentCommand command);

    #endregion
}
