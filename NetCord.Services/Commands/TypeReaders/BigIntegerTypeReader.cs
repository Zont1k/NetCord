﻿using System.Globalization;
using System.Numerics;

namespace NetCord.Services.Commands.TypeReaders;

public class BigIntegerTypeReader<TContext> : CommandTypeReader<TContext> where TContext : ICommandContext
{
    public override Task<object?> ReadAsync(ReadOnlyMemory<char> input, TContext context, CommandParameter<TContext> parameter, CommandServiceConfiguration<TContext> configuration) => Task.FromResult<object?>(BigInteger.Parse(input.Span, NumberStyles.AllowLeadingSign, configuration.CultureInfo));
}
