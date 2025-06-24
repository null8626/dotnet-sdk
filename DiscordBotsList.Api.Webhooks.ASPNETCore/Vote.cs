using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace DiscordBotsList.Api.Webhooks.ASPNETCore
{
    public class Vote
    {
        [JsonPropertyName("bot")] internal ulong botId { get; init; }

        [JsonPropertyName("guild")] internal ulong serverId { get; init; }

        [JsonPropertyName("user")] internal ulong voterId { get; init; }

        [JsonPropertyName("isWeekend")] internal bool isWeekend { get; init; }

        [JsonPropertyName("type")] internal string type { get; init; }

        [JsonPropertyName("query")] internal string query { get; init; }

        public ulong ReceiverId => botId == 0 ? serverId : botId;
        public ulong VoterId => voterId;
        public bool IsWeekend => isWeekend;
        public bool IsTest => type == "test";
        public Dictionary<string, string> Query
        {
            get
            {
                if (query == null)
                {
                    return null;
                }

                var parsedQuery = HttpUtility.ParseQueryString(query);
                return parsedQuery.AllKeys.ToDictionary(key => key, key => parsedQuery[key]);
            }
        }
    }
}