﻿using System.Globalization;

namespace NetCord.Services.Interactions.TypeReaders;

public class SByteTypeReader<TContext> : InteractionTypeReader<TContext> where TContext : InteractionContext
{
    public override Task<object?> ReadAsync(ReadOnlyMemory<char> input, TContext context, InteractionParameter<TContext> parameter, InteractionServiceConfiguration<TContext> configuration) => Task.FromResult<object?>(sbyte.Parse(input.Span, NumberStyles.AllowLeadingSign, configuration.CultureInfo));
}
