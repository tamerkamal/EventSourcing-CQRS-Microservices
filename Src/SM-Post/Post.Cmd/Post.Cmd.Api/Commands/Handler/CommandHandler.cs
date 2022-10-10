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

    public async Task HandleAsync(AddPostCommand command)
    {
        PostAggregate aggregate = new(command.Id, command.RaisedBy);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(AddCommentCommand command)
    {
        PostAggregate aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.AddComment(command.Comment, command.RaisedBy);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditCommentCommand command)
    {
        PostAggregate aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.EditComment(command.CommentId, command.Comment, command.RaisedBy);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditPostTextCommand command)
    {
        PostAggregate aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.EditPostText(command.Text, command.RaisedBy);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(LikePostCommand command)
    {
        PostAggregate aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.LikePost(command.RaisedBy);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(RemovePostCommand command)
    {
        PostAggregate aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.RemovePost(command.RaisedBy);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(RemoveCommentCommand command)
    {
        PostAggregate aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.RemoveComment(command.CommentId, command.RaisedBy);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}