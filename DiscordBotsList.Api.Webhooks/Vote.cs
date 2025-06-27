using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace DiscordBotsList.Api.Webhooks
{
    public class Vote
    {
        [JsonPropertyName("bot")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong botId { get; init; }

        [JsonPropertyName("guild")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong serverId { get; init; }

        [JsonPropertyName("user")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong voterId { get; init; }

        [JsonPropertyName("isWeekend")]
        public bool isWeekend { get; init; }

        [JsonPropertyName("type")]
        public string type { get; init; }

        [JsonPropertyName("query")]
        public string query { get; init; }

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