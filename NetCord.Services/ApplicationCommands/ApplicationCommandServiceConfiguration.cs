﻿namespace NetCord.Services.ApplicationCommands;

public class ApplicationCommandServiceConfiguration<TContext> where TContext : IApplicationCommandContext
{
    public Dictionary<Type, SlashCommandTypeReader<TContext>> TypeReaders { get; } = new()
    #region TypeReaders
    {
        // text types
        {
            typeof(string),
            new TypeReaders.StringTypeReader<TContext>()
        },
        // integral numeric types
        {
            typeof(sbyte),
            new TypeReaders.SByteTypeReader<TContext>()
        },
        {
            typeof(byte),
            new TypeReaders.ByteTypeReader<TContext>()
        },
        {
            typeof(short),
            new TypeReaders.Int16TypeReader<TContext>()
        },
        {
            typeof(ushort),
            new TypeReaders.UInt16TypeReader<TContext>()
        },
        {
            typeof(int),
            new TypeReaders.Int32TypeReader<TContext>()
        },
        {
            typeof(uint),
            new TypeReaders.UInt32TypeReader<TContext>()
        },
        {
            typeof(long),
            new TypeReaders.Int64TypeReader<TContext>()
        },
        {
            typeof(ulong),
            new TypeReaders.UInt64TypeReader<TContext>()
        },
        {
            typeof(nint),
            new TypeReaders.IntPtrTypeReader<TContext>()
        },
        {
            typeof(nuint),
            new TypeReaders.UIntPtrTypeReader<TContext>()
        },
        // floating-point numeric types
        {
            typeof(Half),
            new TypeReaders.HalfTypeReader<TContext>()
        },
        {
            typeof(float),
            new TypeReaders.SingleTypeReader<TContext>()
        },
        {
            typeof(double),
            new TypeReaders.DoubleTypeReader<TContext>()
        },
        {
            typeof(decimal),
            new TypeReaders.DecimalTypeReader<TContext>()
        },
        // other types
        {
            typeof(bool),
            new TypeReaders.BooleanTypeReader<TContext>()
        },
        {
            typeof(User),
            new TypeReaders.UserTypeReader<TContext>()
        },
        {
            typeof(GuildUser),
            new TypeReaders.GuildUserTypeReader<TContext>()
        },
        {
            typeof(Role),
            new TypeReaders.RoleTypeReader<TContext>()
        },
        {
            typeof(CategoryGuildChannel),
            new TypeReaders.ChannelTypeReaders.CategoryGuildChannelTypeReader<TContext>()
        },
        {
            typeof(Channel),
            new TypeReaders.ChannelTypeReaders.ChannelTypeReader<TContext>()
        },
        {
            typeof(DirectoryGuildChannel),
            new TypeReaders.ChannelTypeReaders.DirectoryGuildChannelTypeReader<TContext>()
        },
        {
            typeof(DMChannel),
            new TypeReaders.ChannelTypeReaders.DMChannelTypeReader<TContext>()
        },
        {
            typeof(ForumGuildChannel),
            new TypeReaders.ChannelTypeReaders.ForumGuildChannelTypeReader<TContext>()
        },
        {
            typeof(GroupDMChannel),
            new TypeReaders.ChannelTypeReaders.GroupDMChannelTypeReader<TContext>()
        },
        {
            typeof(AnnouncementGuildChannel),
            new TypeReaders.ChannelTypeReaders.AnnouncementGuildChannelTypeReader<TContext>()
        },
        {
            typeof(AnnouncementGuildThread),
            new TypeReaders.ChannelTypeReaders.AnnouncementGuildThreadTypeReader<TContext>()
        },
        {
            typeof(PrivateGuildThread),
            new TypeReaders.ChannelTypeReaders.PrivateGuildThreadTypeReader<TContext>()
        },
        {
            typeof(PublicGuildThread),
            new TypeReaders.ChannelTypeReaders.PublicGuildThreadTypeReader<TContext>()
        },
        {
            typeof(StageGuildChannel),
            new TypeReaders.ChannelTypeReaders.StageGuildChannelTypeReader<TContext>()
        },
        {
            typeof(TextChannel),
            new TypeReaders.ChannelTypeReaders.TextChannelTypeReader<TContext>()
        },
        {
            typeof(TextGuildChannel),
            new TypeReaders.ChannelTypeReaders.TextGuildChannelTypeReader<TContext>()
        },
        {
            typeof(VoiceGuildChannel),
            new TypeReaders.ChannelTypeReaders.VoiceGuildChannelTypeReader<TContext>()
        },
        {
            typeof(Mentionable),
            new TypeReaders.MentionableTypeReader<TContext>()
        },
        {
            typeof(Attachment),
            new TypeReaders.AttachmentTypeReader<TContext>()
        }
    };
    #endregion

    public SlashCommandTypeReader<TContext> EnumTypeReader { get; init; } = new TypeReaders.EnumTypeReader<TContext>();

    public bool DefaultDMPermission { get; init; } = true;
}
