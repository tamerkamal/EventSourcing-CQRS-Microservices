namespace Post.Cmd.Infrastructure.Dispatchers;

using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, Func<BaseCommand, Task>> _commandHandlers = new();

    public void RegiserHandler<TCommand>(Func<TCommand, Task> commandHandler) where TCommand : BaseCommand
    {
        if (_commandHandlers.ContainsKey(typeof(TCommand)))
        {
            throw new IndexOutOfRangeException("Command handler already registered!");
        }

        _commandHandlers.Add(typeof(TCommand), x => commandHandler((TCommand)x));
    }

#nullable enable
    public async Task SendAsync(BaseCommand command)
    {
        if (!_commandHandlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task>? commandHandler))
        {
            throw new ArgumentNullException(nameof(commandHandler), "Command Handler not registered!");
        }

        await commandHandler(command);
    }
}
