using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordBotsList.Webhooks
{
    /// <summary>
    ///     A project's platform.
    /// </summary>
    public enum Platform
    {
        Discord
    }

    /// <summary>
    ///     Converts platform strings to their enum counterparts.
    /// </summary>
    internal class PlatformConverter : JsonConverter<Platform>
    {
        public override Platform Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                switch (reader.GetString())
                {
                    case "discord": return Platform.Discord;
                }
            }

            throw new InvalidOperationException();
        }

        public override void Write(Utf8JsonWriter writer, Platform platform, JsonSerializerOptions options) => throw new InvalidOperationException();
    }
}