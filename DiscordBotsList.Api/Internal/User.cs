using DiscordBotsList.Api.Objects;
using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Internal
{
    public class User : Entity
    {
        [JsonPropertyName("social")] public SocialConnections Social { get; set; }

        [JsonPropertyName("bio")] public string Biography { get; set; }

        [JsonPropertyName("color")] public string Color { get; set; }

        [JsonPropertyName("supporter")] public bool IsSupporter { get; set; }

        [JsonPropertyName("mod")] public bool IsModerator { get; set; }

        [JsonPropertyName("webMod")] public bool IsWebModerator { get; set; }

        [JsonPropertyName("admin")] public bool IsAdmin { get; set; }

        public SocialConnections Connections => Social;
    }
}