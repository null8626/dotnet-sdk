using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Webhooks
{
    /// <summary>
    ///     An `integration.create` webhook payload.
    /// </summary>
    public class IntegrationCreatePayload
    {
        /// <summary>
        ///     The unique identifier for this connection.
        /// </summary>
        [JsonPropertyName("connection_id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong connectionID { get; init; }
        
        /// <summary>
        ///     The secret used to verify future webhook deliveries.
        /// </summary>
        [JsonPropertyName("webhook_secret")]
        public string secret { get; init; }

        /// <summary>
        ///     The project that the integration refers to.
        /// </summary>
        public PartialProject project { get; init; }
        
        /// <summary>
        ///     The user who triggered this event.
        /// </summary>
        public User user { get; init; }
    }

    /// <summary>
    ///     An `integration.delete` webhook payload.
    /// </summary>
    public class IntegrationDeletePayload
    {
        /// <summary>
        ///     The unique identifier for this connection.
        /// </summary>
        [JsonPropertyName("connection_id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong connectionID { get; init; }
    }
}