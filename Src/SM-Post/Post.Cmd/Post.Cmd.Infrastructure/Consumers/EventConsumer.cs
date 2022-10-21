namespace Post.Cmd.Infrastructure.Consumers;

using System.Reflection;
using System.Text.Json;
using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using Post.Cmd.Domain.Handlers;
using Post.Cmd.Infrastructure.Converters;
using Post.Cmd.Infrastructure.Handlers.EventHandlers;

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

    public void Consume(string topic, string eventHandlerName)
    {
        using (var consumer = new ConsumerBuilder<string, string>(_consumerConfig)
                                       .SetKeyDeserializer(Deserializers.Utf8)
                                       .SetValueDeserializer(Deserializers.Utf8)
                                       .Build())
        {
            consumer.Subscribe(topic);

            while (true)
            {
                var consumeResult = consumer.Consume();

                if (consumeResult?.Message is null) { continue; }

                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<BaseEvent>(consumeResult.Message.Value, options);

                MethodInfo handlerMethod = null;

                switch (eventHandlerName)
                {
                    case nameof(PostEventHandler):
                        {
                            handlerMethod = _postEventHandler.GetType().GetMethod("OnAsync", new Type[] { @event.GetType() });

                            if (handlerMethod is null)
                            {
                                throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler method!");
                            }

                            handlerMethod.Invoke(_postEventHandler, new object[] { @event });

                            break;
                        }
                    case nameof(CommentEventHandler):
                        {
                            handlerMethod = _commentEventHandler.GetType().GetMethod("OnAsync", new Type[] { @event.GetType() });

                            if (handlerMethod is null)
                            {
                                throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler method!");
                            }

                            handlerMethod.Invoke(_commentEventHandler, new object[] { @event });

                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

                consumer.Commit(consumeResult);
            }
        }
    }
}