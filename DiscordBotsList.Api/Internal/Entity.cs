using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Internal
{
    public class Entity
    {
        [JsonPropertyName("avatar")] public string Avatar { get; set; }

        [JsonPropertyName("id")] public ulong Id { get; set; }

        [JsonPropertyName("username")] public string Username { get; set; }

        [JsonPropertyName("discriminator")] public string Discriminator { get; set; }
    }
}