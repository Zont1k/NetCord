﻿using System.Globalization;

namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class Int16TypeReader<TContext> : SlashCommandTypeReader<TContext> where TContext : IApplicationCommandContext
{
    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.Integer;

    public override Task<object?> ReadAsync(string value, TContext context, SlashCommandParameter<TContext> parameter, ApplicationCommandServiceConfiguration<TContext> configuration) => Task.FromResult<object?>(short.Parse(value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture));

    public override double? GetMaxValue(SlashCommandParameter<TContext> parameter) => short.MaxValue;

    public override double? GetMinValue(SlashCommandParameter<TContext> parameter) => short.MinValue;
}
