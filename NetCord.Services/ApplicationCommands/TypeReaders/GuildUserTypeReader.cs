﻿using System.Globalization;

using NetCord.Gateway;

namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class GuildUserTypeReader<TContext> : SlashCommandTypeReader<TContext> where TContext : IApplicationCommandContext
{
    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.User;

    public override Task<object?> ReadAsync(string value, TContext context, SlashCommandParameter<TContext> parameter, ApplicationCommandServiceConfiguration<TContext> configuration)
    {
        var user = ((SlashCommandInteraction)context.Interaction).Data.ResolvedData!.Users![ulong.Parse(value, NumberStyles.None, CultureInfo.InvariantCulture)];
        if (user is GuildUser guildUser)
            return Task.FromResult<object?>(guildUser);
        else
            throw new InvalidOperationException("The user must be in a guild.");
    }
}
