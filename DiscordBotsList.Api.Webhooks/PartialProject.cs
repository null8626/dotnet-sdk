using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Webhooks
{
    /// <summary>
    ///     A brief information on project listed on Top.gg.
    /// </summary>
    public class PartialProject
    {
        /// <summary>
        ///     The project's ID.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong id { get; init; }

        /// <summary>
        ///     The project's type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(ProjectTypeConverter))]
        public ProjectType type { get; init; }

        /// <summary>
        ///     The project's platform.
        /// </summary>
        [JsonPropertyName("platform")]
        [JsonConverter(typeof(PlatformConverter))]
        public Platform platform { get; init; }

        /// <summary>
        ///     The project's platform ID.
        /// </summary>
        [JsonPropertyName("platform_id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong platformID { get; init; }
    }
}