﻿using System.Text.Json.Serialization;

namespace NetCord.JsonModels;

public partial class JsonAuditLog
{
    [JsonPropertyName("application_commands")]
    public JsonApplicationCommand[] ApplicationCommands { get; set; }

    [JsonPropertyName("audit_log_entries")]
    public JsonAuditLogEntry[] AuditLogEntries { get; set; }

    [JsonPropertyName("auto_moderation_rules")]
    public JsonAutoModerationRule[] AutoModerationRules { get; set; }

    [JsonPropertyName("guild_scheduled_events")]
    public JsonGuildScheduledEvent[] GuildScheduledEvents { get; set; }

    [JsonPropertyName("integrations")]
    public JsonIntegration[] Integrations { get; set; }

    [JsonPropertyName("threads")]
    public JsonChannel[] Threads { get; set; }

    [JsonPropertyName("users")]
    public JsonUser[] Users { get; set; }

    [JsonPropertyName("webhooks")]
    public JsonWebhook[] Webhooks { get; set; }

    [JsonSerializable(typeof(JsonAuditLog))]
    public partial class JsonAuditLogSerializerContext : JsonSerializerContext
    {
        public static JsonAuditLogSerializerContext WithOptions { get; } = new(Serialization.Options);
    }
}
