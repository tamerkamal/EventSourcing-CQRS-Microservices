namespace CQRS.Core.Infrastructure;

using CQRS.Core.Commands;

public interface ICommandDispatcher
{
    // Func<T, Task>: 'T' is input parameter, 'Task' is ouput parameter to receive async methods
    void RegiserHandler<T>(Func<T, Task> commandHandler) where T : BaseCommand;
    Task SendAsync(BaseCommand command);
}
