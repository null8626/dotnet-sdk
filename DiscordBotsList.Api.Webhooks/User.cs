using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Webhooks
{
    /// <summary>
    ///     A Top.gg user.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     The user's ID.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong Id { get; internal init; }

        /// <summary>
        ///     The user's name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; internal init; }

        /// <summary>
        ///     The user's avatar URL.
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string Avatar { get; internal init; }

        /// <summary>
        ///     The user's platform ID.
        /// </summary>
        [JsonPropertyName("platform_id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong PlatformId { get; internal init; }
    }
}