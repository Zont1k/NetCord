﻿using System.Globalization;

namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class ByteTypeReader<TContext> : SlashCommandTypeReader<TContext> where TContext : IApplicationCommandContext
{
    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.Integer;

    public override Task<object?> ReadAsync(string value, TContext context, SlashCommandParameter<TContext> parameter, ApplicationCommandServiceConfiguration<TContext> configuration) => Task.FromResult<object?>(byte.Parse(value, NumberStyles.None, CultureInfo.InvariantCulture));

    public override double? GetMaxValue(SlashCommandParameter<TContext> parameter) => byte.MaxValue;

    public override double? GetMinValue(SlashCommandParameter<TContext> parameter) => byte.MinValue;
}
