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
        public ulong id { get; init; }

        /// <summary>
        ///     The user's name.
        /// </summary>
        [JsonPropertyName("name")]
        public string name { get; init; }

        /// <summary>
        ///     The user's avatar URL.
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string avatarURL { get; init; }

        /// <summary>
        ///     The user's platform ID.
        /// </summary>
        [JsonPropertyName("platform_id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong platformID { get; init; }
    }
}