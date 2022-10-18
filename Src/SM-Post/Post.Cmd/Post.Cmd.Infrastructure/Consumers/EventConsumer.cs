namespace Post.Cmd.Infrastructure.Consumers;

using Confluent.Kafka;
using CQRS.Core.Consumers;
using Microsoft.Extensions.Options;
using Post.Cmd.Domain.Handlers;

public class EventConsumer : IEventConsumer
{
    private readonly ConsumerConfig _consumerConfig;
    private readonly IPostEventHandler _postEventHandler;
    private readonly ICommentEventHandler _commentEventHandler;

    public EventConsumer(IOptions<ConsumerConfig> consumerConfig,
                         IPostEventHandler postEventHandler,
                         ICommentEventHandler commentEventHandler)
    {
        _consumerConfig = consumerConfig.Value;
        _postEventHandler = postEventHandler;
        _commentEventHandler = commentEventHandler;
    }

    public void Consume(string topic)
    {
        throw new NotImplementedException();
    }
}
