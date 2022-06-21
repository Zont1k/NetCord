﻿using NetCord.Gateway;
using NetCord.JsonModels;
using NetCord.Rest;

namespace NetCord;

public class Message : RestMessage
{
    public Message(JsonMessage jsonModel, Guild? guild, TextChannel? channel, RestClient client) : base(jsonModel, client)
    {
        GuildId = jsonModel.GuildId ?? jsonModel.MessageReference?.GuildId;
        Guild = guild;
        Channel = channel;
    }

    public static Message CreateFromJson(JsonMessage jsonModel, GatewayClient client)
    {
        Guild? guild;
        TextChannel? channel;
        var guildId = jsonModel.GuildId ?? jsonModel.MessageReference?.GuildId;
        if (guildId.HasValue)
        {
            if (client.Guilds.TryGetValue(jsonModel.GuildId.GetValueOrDefault(), out guild))
            {
                if (guild.Channels.TryGetValue(jsonModel.ChannelId, out var guildChannel))
                    channel = (TextChannel)guildChannel;
                else if (guild.ActiveThreads.TryGetValue(jsonModel.ChannelId, out var thread))
                    channel = thread;
                else
                    channel = null;
            }
            else
            {
                if (client.DMChannels.TryGetValue(jsonModel.ChannelId, out var dMChannel))
                    channel = dMChannel;
                else if (client.GroupDMChannels.TryGetValue(jsonModel.ChannelId, out var groupDMChannel))
                    channel = groupDMChannel;
                else
                    channel = null;
            }
        }
        else
        {
            guild = null;
            if (client.DMChannels.TryGetValue(jsonModel.ChannelId, out var dMChannel))
                channel = dMChannel;
            else if (client.GroupDMChannels.TryGetValue(jsonModel.ChannelId, out var groupDMChannel))
                channel = groupDMChannel;
            else
                channel = null;
        }
        return new(jsonModel, guild, channel, client.Rest);
    }

    public Snowflake? GuildId { get; }
    public Guild? Guild { get; }
    public TextChannel? Channel { get; }

    public string GetJumpUrl() => $"https://discord.com/channels/{(GuildId.HasValue ? GuildId.GetValueOrDefault() : "@me")}/{ChannelId}/{Id}";
    public override string GetJumpUrl(Snowflake? guildId) => GetJumpUrl();
}