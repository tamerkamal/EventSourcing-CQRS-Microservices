using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Events;
using Post.Common.Events;

namespace Post.Cmd.Infrastructure.Converters;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override bool CanConvert(Type typeToConvert)
    {
        var canConvert = typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        return canConvert;
    }

    public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var jsonDocument))
        {
            throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
        }

        if (!jsonDocument.RootElement.TryGetProperty(nameof(Type), out var type))
        {
            throw new JsonException($"Can not detect the {nameof(Type)} discriminator property!");
        }

        var typeDiscriminator = type.GetString();
        var json = jsonDocument.RootElement.GetRawText();

        BaseEvent value = typeDiscriminator switch
        {
            #region PostEvents

            nameof(PostAddedEvent) => JsonSerializer.Deserialize<PostAddedEvent>(json, options),
            nameof(PostLikedEvent) => JsonSerializer.Deserialize<PostLikedEvent>(json, options),
            nameof(PostRemovedEvent) => JsonSerializer.Deserialize<PostRemovedEvent>(json, options),
            nameof(PostTextEditedEvent) => JsonSerializer.Deserialize<PostTextEditedEvent>(json, options),

            #endregion

            #region CommmentEvents

            nameof(CommentAddedEvent) => JsonSerializer.Deserialize<CommentAddedEvent>(json, options),
            nameof(CommentEditedEvent) => JsonSerializer.Deserialize<CommentEditedEvent>(json, options),
            nameof(CommentRemovedEvent) => JsonSerializer.Deserialize<CommentEditedEvent>(json, options),

            #endregion

            _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
        };

        return value;
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
