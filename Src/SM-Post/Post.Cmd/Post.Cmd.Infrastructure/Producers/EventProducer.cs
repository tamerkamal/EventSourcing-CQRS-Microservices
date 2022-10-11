namespace Post.Cmd.Infrastructure.Producers;

using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using CQRS.Core.Events;
using CQRS.Core.Producers;
using Microsoft.Extensions.Options;

public class EventProducer : IEventProducer
{
    private readonly ProducerConfig _producerConfig;

    public EventProducer(IOptions<ProducerConfig> producerConfig)
    {
        _producerConfig = producerConfig.Value;
    }

    public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent
    {
        using (var producer = new ProducerBuilder<string, string>(_producerConfig)
                                  .SetKeySerializer(Serializers.Utf8)
                                  .SetValueSerializer(Serializers.Utf8)
                                  .Build())
        {
            Message<string, string> eventMessage = new()
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(@event, @event.GetType())
            };

            var deliveryReuslt = await producer.ProduceAsync(topic, eventMessage);

            if (deliveryReuslt.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Could not produce {@event.GetType()} message to topic '{topic}' due to the following reason {deliveryReuslt.Message}.");
            }
        }
    }
}
