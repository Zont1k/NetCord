﻿using System.Text.Json.Serialization;

namespace NetCord.Rest;

internal partial class DMChannelProperties
{
    public DMChannelProperties(ulong userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// The recipient to open a DM channel with.
    /// </summary>
    [JsonPropertyName("recipient_id")]
    public ulong UserId { get; set; }

    [JsonSerializable(typeof(DMChannelProperties))]
    public partial class DMChannelPropertiesSerializerContext : JsonSerializerContext
    {
        public static DMChannelPropertiesSerializerContext WithOptions { get; } = new(Serialization.Options);
    }
}
