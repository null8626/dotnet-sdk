using System.Text.Json;
using System.Text.Json.Serialization;

namespace Topgg.Sdk.Webhooks.Payloads;

internal class Payload
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("data")]
    public JsonElement Data { get; init; }
}