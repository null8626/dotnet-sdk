using System;
using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Webhooks
{
    /// <summary>
    ///     A `vote.create` webhook payload.
    /// </summary>
    public class VoteCreatePayload
    {
        /// <summary>
        ///     The vote's ID.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong id { get; init; }

        /// <summary>
        ///     The number of votes this vote counted for. This is a rounded integer value which determines how many points this individual vote was worth.
        /// </summary>
        [JsonPropertyName("weight")]
        public int weight { get; init; }

        /// <summary>
        ///     When the vote was cast.
        /// </summary>
        [JsonPropertyName("voted_at")]
        public DateTime votedAt { get; init; }

        /// <summary>
        ///     When the vote expires (the user can vote again.)
        /// </summary>
        [JsonPropertyName("expires_at")]
        public DateTime expiresAt { get; init; }
        
        /// <summary>
        ///     The project that received this vote.
        /// </summary>
        [JsonPropertyName("project")]
        public PartialProject project { get; init; }
        
        /// <summary>
        ///     The user who voted for this project.
        /// </summary>
        [JsonPropertyName("user")]
        public User user { get; init; }
    }
}