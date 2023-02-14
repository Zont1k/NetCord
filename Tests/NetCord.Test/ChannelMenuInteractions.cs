﻿using NetCord.Services.Interactions;

namespace NetCord.Test;

public class ChannelMenuInteractions : InteractionModule<ChannelMenuInteractionContext>
{
    [Interaction("channels")]
    public Task ChannelsAsync()
    {
        return RespondAsync(InteractionCallback.ChannelMessageWithSource($"You selected: {string.Join(", ", Context.SelectedChannels)}"));
    }
}
