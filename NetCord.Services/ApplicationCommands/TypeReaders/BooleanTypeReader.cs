﻿namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class BooleanTypeReader<TContext> : SlashCommandTypeReader<TContext> where TContext : IApplicationCommandContext
{
    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.Boolean;

    public override Task<object?> ReadAsync(string value, TContext context, SlashCommandParameter<TContext> parameter, ApplicationCommandServiceConfiguration<TContext> configuration) => Task.FromResult<object?>(bool.Parse(value));
}
