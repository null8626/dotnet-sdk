using System;
using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Internal
{
    public class Vote
    {
        [JsonPropertyName("createdAt")]
        public DateTime VotedAt { get; internal set; }

        [JsonPropertyName("expiresAt")]
        public DateTime ExpiresAt { get; internal set; }

        [JsonPropertyName("weight")]
        public double Weight { get; internal set; }
    }
}