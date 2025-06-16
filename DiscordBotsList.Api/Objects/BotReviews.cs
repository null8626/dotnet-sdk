using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Objects
{
    public class BotReviews
    {
        [JsonPropertyName("averageScore")] public double AverageScore { get; internal set; }

        [JsonPropertyName("count")] public int Count { get; internal set; }
    }
}