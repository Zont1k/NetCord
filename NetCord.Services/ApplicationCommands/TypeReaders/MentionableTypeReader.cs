﻿using System.Globalization;

using NetCord.Gateway;

namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class MentionableTypeReader<TContext> : SlashCommandTypeReader<TContext> where TContext : IApplicationCommandContext
{
    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.Mentionable;

    public override Task<object?> ReadAsync(string value, TContext context, SlashCommandParameter<TContext> parameter, ApplicationCommandServiceConfiguration<TContext> configuration)
    {
        var slashInteraction = (SlashCommandInteraction)context.Interaction;
        var roles = slashInteraction.Data.ResolvedData!.Roles;
        var id = ulong.Parse(value, NumberStyles.None, CultureInfo.InvariantCulture);
        if (roles != null && roles.TryGetValue(id, out var role))
            return Task.FromResult<object?>(new Mentionable(role));
        else
            return Task.FromResult<object?>(new Mentionable(slashInteraction.Data.ResolvedData!.Users![id]));
    }
}
