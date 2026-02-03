using System.Collections.Generic;
using System.Text.Json.Serialization;
using DiscordBotsList.Api.Serialization;

namespace DiscordBotsList.Api.Internal
{
    public class Project
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong Id { get; internal set; }

        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        [JsonPropertyName("platform")]
        public string Platform { get; internal set; }

        [JsonPropertyName("type")]
        public string Type { get; internal set; }

        [JsonPropertyName("headline")]
        public string Headline { get; internal set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; internal set; }

        [JsonPropertyName("votes")]
        public int CurrentVotes { get; internal set; }

        [JsonPropertyName("votes_total")]
        public int TotalVotes { get; internal set; }

        [JsonPropertyName("review_score")]
        public float ReviewScore { get; internal set; }

        [JsonPropertyName("review_count")]
        public int ReviewCount { get; internal set; }
    }
}