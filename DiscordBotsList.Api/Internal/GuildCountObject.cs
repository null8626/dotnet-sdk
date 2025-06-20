using DiscordBotsList.Api.Objects;
using System.Text.Json.Serialization;

namespace DiscordBotsList.Api.Internal
{
    internal class ServerCountObject
    {
        [JsonPropertyName("server_count")] internal int serverCount;

        public ServerCountObject(int count)
        {
            serverCount = count;
        }
    }

    internal class BotStatsObject : IDblBotStats
    {
        [JsonPropertyName("server_count")] internal int serverCount { get; set; }
        public int ServerCount => serverCount;
    }
}