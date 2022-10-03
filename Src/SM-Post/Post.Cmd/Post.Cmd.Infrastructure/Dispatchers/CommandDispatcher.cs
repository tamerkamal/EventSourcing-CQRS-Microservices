namespace Post.Cmd.Infrastructure.Dispatchers;

using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;
public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, Func<BaseCommand, Task>> _commandHandlers = new();

    public void RegiserHandler<T>(Func<T, Task> commandHandler) where T : BaseCommand
    {
        if (_commandHandlers.ContainsKey(typeof(T)))
        {
            throw new IndexOutOfRangeException("Command handler already registered!");
        }

        _commandHandlers.Add(typeof(T), x => commandHandler((T)x));
    }

    public async Task SendAsync(BaseCommand command)
    {
        if (!_commandHandlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task>? commandHandler))
        {
            throw new ArgumentNullException(nameof(commandHandler), "Command Handler not registered!");
        }

        await commandHandler(command);
    }
}
