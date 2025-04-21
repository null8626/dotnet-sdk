using DiscordBotsList.Api.Objects;
using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Internal
{
    internal class GuildCountObject
    {
        [JsonPropertyName("server_count")] internal int guildCount;

        public GuildCountObject(int count)
        {
            guildCount = count;
        }
    }

    internal class BotStatsObject : IDblBotStats
    {
        [JsonPropertyName("server_count")] internal int guildCount { get; set; }
        public int GuildCount => guildCount;
    }
}