﻿namespace NetCord.Services.Interactions;

public abstract class InteractionTypeReader<TContext> : IInteractionTypeReader where TContext : InteractionContext
{
    public abstract Task<object> ReadAsync(string input, TContext context, InteractionParameter<TContext> parameter, InteractionServiceOptions<TContext> options);
}

internal interface IInteractionTypeReader
{
}