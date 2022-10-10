namespace Post.Cmd.Api.Commands.Handler;

using Post.Cmd.Api.Commands;

public interface ICommandHandler
{
    Task HandleAsync(AddCommentCommand command);
    Task HandleAsync(AddPostCommand command);
    Task HandleAsync(EditCommentCommand command);
    Task HandleAsync(EditPostTextCommand command);
    Task HandleAsync(LikePostCommand command);
    Task HandleAsync(RemoveCommentCommand command);
    Task HandleAsync(RemovePostCommand command);
}
