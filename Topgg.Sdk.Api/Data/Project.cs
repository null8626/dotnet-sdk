using System.Collections.Generic;
using System.Text.Json.Serialization;
using Topgg.Sdk.Api.Serialization;

namespace Topgg.Sdk.Api.Data;

/// <summary>A project's platform.</summary>
public enum Platform
{
    Discord
}

/// <summary>A project's type.</summary>
public enum ProjectType
{
    Bot,
    Server
}

/// <summary>A project listed on Top.gg.</summary>
public class Project
{
    /// <summary>The project's ID.</summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(ULongToStringConverter))]
    public ulong Id { get; internal init; }

    /// <summary>The project's name sourced from the external platform.</summary>
    [JsonPropertyName("name")]
    public string Name { get; internal init; }

    /// <summary>The project's platform.</summary>
    [JsonPropertyName("platform")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Platform Platform { get; internal init; }

    /// <summary>The project's type.</summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectType Type { get; internal init; }

    /// <summary>The project's short description.</summary>
    [JsonPropertyName("headline")]
    public string Headline { get; internal init; }

    /// <summary>The project's tag IDs.</summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; internal init; }

    /// <summary>The project's current vote count that affects the project's ranking.</summary>
    [JsonPropertyName("votes")]
    public int Votes { get; internal init; }

    /// <summary>The project's total vote count.</summary>
    [JsonPropertyName("votes_total")]
    public int VotesTotal { get; internal init; }

    /// <summary>The project's review score out of 5.</summary>
    [JsonPropertyName("review_score")]
    public float ReviewScore { get; internal init; }

    /// <summary>The project's total review count.</summary>
    [JsonPropertyName("review_count")]
    public int ReviewCount { get; internal init; }
}

/// <summary>A brief information on a project listed on Top.gg.</summary>
public class PartialProject
{
    /// <summary>The project's ID.</summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(ULongToStringConverter))]
    public ulong Id { get; internal init; }

    /// <summary>The project's ID.</summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProjectType Type { get; internal init; }

    /// <summary>The project's platform.</summary>
    [JsonPropertyName("platform")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Platform Platform { get; internal init; }

    /// <summary>The project's platform ID.</summary>
    [JsonPropertyName("platform_id")]
    [JsonConverter(typeof(ULongToStringConverter))]
    public ulong PlatformId { get; internal init; }
}