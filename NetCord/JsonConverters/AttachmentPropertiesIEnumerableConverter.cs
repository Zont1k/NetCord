﻿using System.Text.Json;
using System.Text.Json.Serialization;

using NetCord.Rest;

namespace NetCord.JsonConverters;

internal class AttachmentPropertiesIEnumerableConverter : JsonConverter<IEnumerable<AttachmentProperties>>
{
    public override IEnumerable<AttachmentProperties>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, IEnumerable<AttachmentProperties> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        int i = 0;
        foreach (var attachment in value)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", i);
            writer.WriteString("filename", attachment.FileName);
            if (attachment.Description != null)
                writer.WriteString("description", attachment.Description);
            if (attachment is GoogleCloudPlatformAttachmentProperties googleCloudPlatformAttachment)
                writer.WriteString("uploaded_filename", googleCloudPlatformAttachment.UploadedFileName);
            writer.WriteEndObject();
            i++;
        }
        writer.WriteEndArray();
    }
}
