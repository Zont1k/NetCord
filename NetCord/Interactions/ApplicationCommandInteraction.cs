﻿using NetCord.JsonModels;
using NetCord.Rest;

namespace NetCord;

public abstract class ApplicationCommandInteraction : Interaction
{
    public abstract override ApplicationCommandInteractionData Data { get; }

    public ApplicationCommandInteraction(JsonInteraction jsonModel, Guild? guild, TextChannel? channel, RestClient client) : base(jsonModel, guild, channel, client)
    {
    }
}