﻿using NetCord.JsonModels;
using NetCord.Rest;

namespace NetCord.Gateway;

public class ChannelMenuInteraction : EntityMenuInteraction
{
    public ChannelMenuInteraction(JsonInteraction jsonModel, Guild? guild, TextChannel? channel, RestClient client) : base(jsonModel, guild, channel, client)
    {
    }
}
