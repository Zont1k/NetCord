﻿using System.Text.Json.Serialization;

namespace NetCord.Rest;

public partial class ModalProperties
{
    [JsonPropertyName("custom_id")]
    public string CustomId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonConverter(typeof(JsonConverters.TextInputPropertiesIEnumerableConverter))]
    [JsonPropertyName("components")]
    public IEnumerable<TextInputProperties> Components { get; set; }

    public ModalProperties(string customId, string title, IEnumerable<TextInputProperties> components)
    {
        CustomId = customId;
        Title = title;
        Components = components;
    }

    [JsonSerializable(typeof(ModalProperties))]
    public partial class ModalPropertiesSerializerContext : JsonSerializerContext
    {
        public static ModalPropertiesSerializerContext WithOptions { get; } = new(Serialization.Options);
    }
}
