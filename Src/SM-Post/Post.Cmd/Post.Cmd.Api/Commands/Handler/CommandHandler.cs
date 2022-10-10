namespace Post.Cmd.Api.Commands.Handler;

using CQRS.Core.Handlers;
using Post.Cmd.Domain.Aggregates;

public class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public Task HandleAsync(AddCommentCommand command)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(AddPostCommand command)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(EditCommentCommand command)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(EditPostTextCommand command)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(LikePostCommand command)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(RemoveCommentCommand command)
    {
        throw new NotImplementedException();
    }

    public Task HandleAsync(RemovePostCommand command)
    {
        throw new NotImplementedException();
    }
}
