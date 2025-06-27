using System.Text.Json.Serialization;
using DiscordBotsList.Api.Objects;

namespace DiscordBotsList.Api.Internal
{
    public class Entity: IDblEntity
    {
        [JsonPropertyName("avatar")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}