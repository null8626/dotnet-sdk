using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Objects
{
    public class WeekendObject
    {
        [JsonPropertyName("is_weekend")] public bool Weekend { get; set; }
    }
}