namespace Post.Cmd.Infrastructure.Consumers;

using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Post.Cmd.Infrastructure.Handlers.EventHandlers;

/// <summary>
/// Background service
/// </summary>
public class ConsumerHostedService : IHostedService
{
    private readonly ILogger<ConsumerHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ConsumerHostedService(ILogger<ConsumerHostedService> logger,
                                 IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    #region  Public methods

    /// <summary>
    /// Called when the container Starts
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event consumer service running!");

        ConsumeEvents(cancellationToken);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Called when the container stops
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event consumer service stopping!");

        return Task.CompletedTask;
    }

    #endregion

    #region Private methods

    private Task ConsumeEvents(CancellationToken cancellationToken)
    {
        using (IServiceScope serviceScope = _serviceProvider.CreateScope())
        {
            var eventConsumer = serviceScope.ServiceProvider.GetRequiredService<IEventConsumer>();
            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

            #region Consume Events

            Task.Run(() => eventConsumer.Consume(topic, nameof(CommentEventHandler)), cancellationToken);
            Task.Run(() => eventConsumer.Consume(topic, nameof(PostEventHandler)), cancellationToken);

            #endregion

            return Task.CompletedTask;
        }
    }

    #endregion
}
