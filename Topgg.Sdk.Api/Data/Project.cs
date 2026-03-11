using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Topgg.Sdk.Api.Serialization;

namespace Topgg.Sdk.Api.Data;

/// <summary>
///     A project's platform.
/// </summary>
public enum Platform
{
    Discord
}

/// <summary>
///     Converts platform strings to their enum counterparts.
/// </summary>
internal class PlatformConverter : JsonConverter<Platform>
{
    public override Platform Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            switch (reader.GetString())
            {
                case "discord": return Platform.Discord;
            }
        }

        throw new InvalidOperationException();
    }

    public override void Write(Utf8JsonWriter writer, Platform platform, JsonSerializerOptions options) => throw new InvalidOperationException();
}

/// <summary>
///     A project's type.
/// </summary>
public enum ProjectType
{
    DiscordBot,
    DiscordServer
}

/// <summary>
///     Converts project type strings to their enum counterparts.
/// </summary>
internal class ProjectTypeConverter : JsonConverter<ProjectType>
{
    public override ProjectType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            switch (reader.GetString())
            {
                case "bot": return ProjectType.DiscordBot;
                case "server": return ProjectType.DiscordServer;
            }
        }

        throw new InvalidOperationException();
    }

    public override void Write(Utf8JsonWriter writer, ProjectType type, JsonSerializerOptions options) => throw new InvalidOperationException();
}

/// <summary>
///     A project listed on Top.gg.
/// </summary>
public class Project
{
    /// <summary>
    ///     The project's ID.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(ULongToStringConverter))]
    public ulong Id { get; internal init; }

    /// <summary>
    ///     The project's name sourced from the external platform.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; internal init; }

    /// <summary>
    ///     The project's platform.
    /// </summary>
    [JsonPropertyName("platform")]
    public Platform Platform { get; internal init; }

    /// <summary>
    ///     The project's type.
    /// </summary>
    [JsonPropertyName("type")]
    public ProjectType Type { get; internal init; }

    /// <summary>
    ///     The project's short description.
    /// </summary>
    [JsonPropertyName("headline")]
    public string Headline { get; internal init; }

    /// <summary>
    ///     The project's tag IDs.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; internal init; }

    /// <summary>
    ///     The project's current vote count that affects the project's ranking.
    /// </summary>
    [JsonPropertyName("votes")]
    public int Votes { get; internal init; }

    /// <summary>
    ///     The project's total vote count.
    /// </summary>
    [JsonPropertyName("votes_total")]
    public int VotesTotal { get; internal init; }

    /// <summary>
    ///     The project's review score out of 5.
    /// </summary>
    [JsonPropertyName("review_score")]
    public float ReviewScore { get; internal init; }

    /// <summary>
    ///     The project's total review count.
    /// </summary>
    [JsonPropertyName("review_count")]
    public int ReviewCount { get; internal init; }
}

/// <summary>
///     A brief information on a project listed on Top.gg.
/// </summary>
public class PartialProject
{
    /// <summary>
    ///     The project's ID.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(ULongToStringConverter))]
    public ulong Id { get; internal init; }

    /// <summary>
    ///     The project's ID.
    /// </summary>
    [JsonPropertyName("type")]
    public ProjectType Type { get; internal init; }

    /// <summary>
    ///     The project's platform.
    /// </summary>
    [JsonPropertyName("platform")]
    public Platform Platform { get; internal init; }

    /// <summary>
    ///     The project's platform ID.
    /// </summary>
    [JsonPropertyName("platform_id")]
    [JsonConverter(typeof(ULongToStringConverter))]
    public ulong PlatformId { get; internal init; }
}