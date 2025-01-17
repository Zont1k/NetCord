﻿using System.Globalization;

using NetCord.Services.ApplicationCommands;

namespace NetCord.Test.SlashCommands;

internal class PercentageTypeReader : SlashCommandTypeReader<SlashCommandContext>
{
    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.Integer;

    public override Task<object?> ReadAsync(string value, SlashCommandContext context, SlashCommandParameter<SlashCommandContext> parameter, ApplicationCommandServiceConfiguration<SlashCommandContext> configuration)
        => Task.FromResult((object?)byte.Parse(value, NumberStyles.None, CultureInfo.InvariantCulture));

    public override double? GetMaxValue(SlashCommandParameter<SlashCommandContext> parameter) => 100;

    public override double? GetMinValue(SlashCommandParameter<SlashCommandContext> parameter) => 0;
}
