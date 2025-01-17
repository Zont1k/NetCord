﻿using System.Reflection;

using NetCord.Gateway;
using NetCord.Rest;

namespace NetCord.Services.ApplicationCommands;

public class ApplicationCommandService<TContext> : IService where TContext : IApplicationCommandContext
{
    private readonly ApplicationCommandServiceConfiguration<TContext> _configuration;
    internal readonly List<ApplicationCommandInfo<TContext>> _globalCommandsToCreate = new();
    internal readonly List<ApplicationCommandInfo<TContext>> _guildCommandsToCreate = new();
    private readonly Dictionary<ulong, ApplicationCommandInfo<TContext>> _commands = new();

    public IReadOnlyDictionary<ulong, ApplicationCommandInfo<TContext>> Commands
    {
        get
        {
            lock (_commands)
                return _commands.ToDictionary(c => c.Key, c => c.Value);
        }
    }

    public ApplicationCommandService(ApplicationCommandServiceConfiguration<TContext>? configuration = null)
    {
        _configuration = configuration ?? new();
    }

    public void AddModules(Assembly assembly)
    {
        Type baseType = typeof(ApplicationCommandModule<TContext>);
        lock (_commands)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAssignableTo(baseType))
                    AddModuleCore(type);
            }
        }
    }

    public void AddModule(Type type)
    {
        if (!type.IsAssignableTo(typeof(ApplicationCommandModule<TContext>)))
            throw new InvalidOperationException($"Modules must inherit from '{nameof(ApplicationCommandModule<TContext>)}'.");

        lock (_commands)
            AddModuleCore(type);
    }

    private void AddModuleCore(Type type)
    {
        foreach (var method in type.GetMethods())
        {
            SlashCommandAttribute? slashCommandAttribute = method.GetCustomAttribute<SlashCommandAttribute>();
            if (slashCommandAttribute != null)
                AddCommandInfo(new(method, slashCommandAttribute, _configuration));

            UserCommandAttribute? userCommandAttribute = method.GetCustomAttribute<UserCommandAttribute>();
            if (userCommandAttribute != null)
                AddCommandInfo(new(method, userCommandAttribute, _configuration));

            MessageCommandAttribute? messageCommandAttribute = method.GetCustomAttribute<MessageCommandAttribute>();
            if (messageCommandAttribute != null)
                AddCommandInfo(new(method, messageCommandAttribute, _configuration));
        }

        void AddCommandInfo(ApplicationCommandInfo<TContext> applicationCommandInfo)
        {
            if (applicationCommandInfo.GuildId.HasValue)
                _guildCommandsToCreate.Add(applicationCommandInfo);
            else
                _globalCommandsToCreate.Add(applicationCommandInfo);
        }
    }

    public async Task<IReadOnlyList<ApplicationCommand>> CreateCommandsAsync(RestClient client, ulong applicationId, bool includeGuildCommands = false, RequestProperties? properties = null)
    {
        List<ApplicationCommand> list = new(_globalCommandsToCreate.Count + _guildCommandsToCreate.Count);
        var e = (await client.BulkOverwriteGlobalApplicationCommandsAsync(applicationId, _globalCommandsToCreate.Select(c => c.GetRawValue()), properties).ConfigureAwait(false)).Zip(_globalCommandsToCreate);
        lock (_commands)
        {
            foreach (var (First, Second) in e)
            {
                _commands[First.Key] = Second;
                list.Add(First.Value);
            }
        }

        if (includeGuildCommands)
        {
            foreach (var c in _guildCommandsToCreate.GroupBy(c => c.GuildId))
            {
                var rawGuildCommands = c.Select(v => v.GetRawValue());

                var newCommands = await client.BulkOverwriteGuildApplicationCommandsAsync(applicationId, c.Key.GetValueOrDefault(), rawGuildCommands, properties).ConfigureAwait(false);
                lock (_commands)
                {
                    foreach (var (First, Second) in newCommands.Zip(c))
                    {
                        _commands[First.Key] = Second;
                        list.Add(First.Value);
                    }
                }
            }
        }
        return list;
    }

    public IEnumerable<ApplicationCommandProperties> GetGlobalCommandProperties()
        => _globalCommandsToCreate.Select(c => c.GetRawValue());

    public IEnumerable<IGrouping<ulong, ApplicationCommandProperties>> GetGuildCommandProperties()
        => _guildCommandsToCreate.GroupBy(c => c.GuildId.GetValueOrDefault(), c => c.GetRawValue());

    internal void AddCommands(IEnumerable<(ApplicationCommand Command, IApplicationCommandInfo CommandInfo)> commands)
    {
        lock (_commands)
        {
            foreach (var (command, commandInfo) in commands)
                _commands[command.Id] = (ApplicationCommandInfo<TContext>)commandInfo;
        }
    }

    internal void AddCommand((ApplicationCommand Command, IApplicationCommandInfo CommandInfo) command)
    {
        lock (_commands)
            _commands[command.Command.Id] = (ApplicationCommandInfo<TContext>)command.CommandInfo;
    }

    public async Task ExecuteAsync(TContext context)
    {
        var interaction = context.Interaction;
        ApplicationCommandInfo<TContext>? command;
        lock (_commands)
        {
            if (!_commands.TryGetValue(interaction.Data.Id, out command!))
                throw new ApplicationCommandNotFoundException();
        }

        await command.EnsureCanExecuteAsync(context).ConfigureAwait(false);

        bool isStatic = command.Static;
        var parameters = command.Parameters;
        object?[] values;

        if (interaction is SlashCommandInteraction slashCommandInteraction)
        {
            int parametersCount = parameters!.Count;
            ArraySegment<object?> parametersToPass;
            if (isStatic)
            {
                values = new object?[parametersCount];
                parametersToPass = values;
            }
            else
            {
                values = new object?[parametersCount + 1];
                parametersToPass = new(values, 1, parametersCount);
            }
            var options = slashCommandInteraction.Data.Options;
            int optionsCount = options.Count;
            int parameterIndex = 0;
            for (int optionIndex = 0; optionIndex < optionsCount; parameterIndex++)
            {
                var parameter = parameters[parameterIndex];
                var option = options[optionIndex];
                object? value;
                if (parameter.Name != option.Name)
                    value = parameter.DefaultValue;
                else
                {
                    value = await parameter.TypeReader.ReadAsync(option.Value!, context, parameter, _configuration).ConfigureAwait(false);
                    optionIndex++;
                }
                await parameter.EnsureCanExecuteAsync(value, context).ConfigureAwait(false);
                parametersToPass[parameterIndex] = value;
            }
            while (parameterIndex < parametersCount)
            {
                var parameter = parameters[parameterIndex];
                var value = parameter.DefaultValue;
                await parameter.EnsureCanExecuteAsync(value, context).ConfigureAwait(false);
                parametersToPass[parameterIndex] = value;
                parameterIndex++;
            }

            if (!isStatic)
            {
                var methodClass = (ApplicationCommandModule<TContext>)Activator.CreateInstance(command.DeclaringType)!;
                methodClass.Context = context;
                values[0] = methodClass;
            }
        }
        else
        {
            if (isStatic)
                values = Array.Empty<object?>();
            else
            {
                var methodClass = (ApplicationCommandModule<TContext>)Activator.CreateInstance(command.DeclaringType)!;
                methodClass.Context = context;
                values = new object?[]
                {
                    methodClass,
                };
            }
        }
        await command.InvokeAsync(values).ConfigureAwait(false);
    }

    public async Task ExecuteAutocompleteAsync(ApplicationCommandAutocompleteInteraction autocompleteInteraction)
    {
        var parameter = autocompleteInteraction.Data.Options.First(o => o.Focused);
        ApplicationCommandInfo<TContext> command;
        lock (_commands)
        {
            if (!_commands.TryGetValue(autocompleteInteraction.Data.Id, out command!))
                throw new ApplicationCommandNotFoundException();
        }
        var autocompletes = command.Autocompletes!;
        if (!autocompletes.TryGetValue(parameter.Name, out var autocompleteProvider))
            throw new AutocompleteNotFoundException();
        var choices = await autocompleteProvider.GetChoicesAsync(parameter, autocompleteInteraction).ConfigureAwait(false);
        await autocompleteInteraction.SendResponseAsync(InteractionCallback.ApplicationCommandAutocompleteResult(choices)).ConfigureAwait(false);
    }
}
