﻿using NetCord.Rest;
using NetCord.Services;
using NetCord.Services.Commands;

namespace NetCord.Test.Commands.Administrative;

[RequireUserPermissions<CommandContext>(Permissions.ModerateUsers), RequireBotPermissions<CommandContext>(Permissions.ModerateUsers)]
public class MuteCommands : CommandModule<CommandContext>
{
    [Command("mute")]
    public async Task Mute([CanManage] GuildUser user, TimeSpan time, [Remainder] string? reason = null)
    {
        await user.TimeOutAsync(DateTimeOffset.UtcNow.Add(time), new() { AuditLogReason = reason });

        ActionRowProperties actionRow = new(new List<ButtonProperties>
        {
            new ActionButtonProperties($"unmute:{user.Id}", "Unmute", ButtonStyle.Danger),
        });
        MessageProperties message = new()
        {
            Content = Format.Bold($"{user} got muted").ToString(),
            Components = new List<ComponentProperties>()
            {
                actionRow
            },
            MessageReference = new(Context.Message.Id),
            AllowedMentions = AllowedMentionsProperties.None,
        };
        await SendAsync(message);
    }

    [Command("unmute")]
    public async Task Unmute([CanManage] GuildUser user, [Remainder] string? reason = null)
    {
        await user.TimeOutAsync(default, new() { AuditLogReason = reason });
        await ReplyAsync($"{user} got unmuted");
    }
}
