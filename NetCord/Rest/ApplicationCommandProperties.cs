﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetCord;

public class SlashCommandProperties : ApplicationCommandProperties
{
    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("options")]
    public IEnumerable<ApplicationCommandOptionProperties>? Options { get; set; }

    public SlashCommandProperties(string name, string description) : base(name)
    {
        Description = description;
    }
}

public class UserCommandProperties : ApplicationCommandProperties
{
    [JsonPropertyName("type")]
    public ApplicationCommandType Type => ApplicationCommandType.User;

    public UserCommandProperties(string name) : base(name)
    {
    }
}

public class MessageCommandProperties : ApplicationCommandProperties
{
    [JsonPropertyName("type")]
    public ApplicationCommandType Type => ApplicationCommandType.Message;

    public MessageCommandProperties(string name) : base(name)
    {
    }
}

[JsonConverter(typeof(ApplicationCommandPropertiesConverter))]
public abstract class ApplicationCommandProperties
{
    private protected ApplicationCommandProperties(string name)
    {
        Name = name;
    }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("default_permission")]
    public bool? DefaultPermission { get; set; }

    private class ApplicationCommandPropertiesConverter : JsonConverter<ApplicationCommandProperties>
    {
        public override ApplicationCommandProperties? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
        public override void Write(Utf8JsonWriter writer, ApplicationCommandProperties value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case SlashCommandProperties slashCommandProperties:
                    JsonSerializer.Serialize(writer, slashCommandProperties);
                    break;
                case UserCommandProperties userCommandProperties:
                    JsonSerializer.Serialize(writer, userCommandProperties);
                    break;
                case MessageCommandProperties messageCommandProperties:
                    JsonSerializer.Serialize(writer, messageCommandProperties);
                    break;
            }
        }
    }
}