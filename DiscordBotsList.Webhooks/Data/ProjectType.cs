using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordBotsList.Webhooks.Data
{
    /// <summary>
    ///     A project's type.
    /// </summary>
    public enum ProjectType
    {
        DiscordBot,
        DiscordServer
    }

    /// <summary>
    ///     Converts project type strings to their enum counterparts.
    /// </summary>
    internal class ProjectTypeConverter : JsonConverter<ProjectType>
    {
        public override ProjectType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                switch (reader.GetString())
                {
                    case "bot": return ProjectType.DiscordBot;
                    case "server": return ProjectType.DiscordServer;
                }
            }

            throw new InvalidOperationException();
        }

        public override void Write(Utf8JsonWriter writer, ProjectType type, JsonSerializerOptions options) => throw new InvalidOperationException();
    }
}