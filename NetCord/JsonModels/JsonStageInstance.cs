﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels;

public partial class JsonStageInstance : JsonEntity
{
    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong ChannelId { get; set; }

    [JsonPropertyName("topic")]
    public string Topic { get; set; }

    [JsonPropertyName("privacy_level")]
    public StageInstancePrivacyLevel PrivacyLevel { get; set; }

    [JsonPropertyName("discoverable_disabled")]
    public bool DiscoverableDisabled { get; set; }

    [JsonSerializable(typeof(JsonStageInstance))]
    public partial class JsonStageInstanceSerializerContext : JsonSerializerContext
    {
        public static JsonStageInstanceSerializerContext WithOptions { get; } = new(Serialization.Options);
    }
}
