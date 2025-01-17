﻿using System.Text.Json.Serialization;

namespace NetCord.Rest;

internal partial class GuildBanProperties
{
    public GuildBanProperties(int deleteMessageSeconds)
    {
        DeleteMessageSeconds = deleteMessageSeconds;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("delete_message_seconds")]
    public int DeleteMessageSeconds { get; set; }

    [JsonSerializable(typeof(GuildBanProperties))]
    public partial class GuildBanPropertiesSerializerContext : JsonSerializerContext
    {
        public static GuildBanPropertiesSerializerContext WithOptions { get; } = new(Serialization.Options);
    }
}
