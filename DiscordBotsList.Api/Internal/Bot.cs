using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using DiscordBotsList.Api.Objects;

namespace DiscordBotsList.Api.Internal
{
    public class Bot : Entity, IDblBot
    {
        internal DiscordBotListApi api;

        [JsonPropertyName("clientid")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong clientId { get; set; }

        [JsonPropertyName("prefix")]
        public string prefix { get; set; }

        [JsonPropertyName("shortdesc")]
        public string shortDescription { get; set; }

        [JsonPropertyName("longdesc")]
        public string longDescription { get; set; }

        [JsonPropertyName("tags")]
        public List<string> tags { get; set; }

        [JsonPropertyName("website")]
        public string websiteUrl { get; set; }

        [JsonPropertyName("support")]
        public string supportUrl { get; set; }

        [JsonPropertyName("github")]
        public string githubUrl { get; set; }

        [JsonPropertyName("owners")]
        public List<ulong> owners { get; set; }

        [JsonPropertyName("invite")]
        public string inviteUrl { get; set; }

        [JsonPropertyName("date")]
        public DateTime submittedAt { get; set; }

        [JsonPropertyName("server_count")]
        public int? serverCount { get; set; }

        [JsonPropertyName("vanity")]
        public string vanity { get; set; }

        [JsonPropertyName("points")]
        public int points { get; set; }

        [JsonPropertyName("monthlyPoints")]
        public int monthlyPoints { get; set; }

        [JsonPropertyName("reviews")]
        public BotReviews reviews { get; set; }

        public string VanityTag => vanity;

        public ulong ClientId => clientId;

        public DateTime SubmittedAt => submittedAt;

        public int? ServerCount => serverCount;

        public string GithubUrl => githubUrl;

        public string InviteUrl => inviteUrl ?? $"https://discord.com/oauth2/authorize?&client_id={Id}&scope=bot";

        public string LongDescription => longDescription;

        public string PrefixUsed => prefix;

        public List<ulong> OwnerIds => owners.ToList();

        public int Points => points;

        public int MonthlyPoints => monthlyPoints;

        public string ShortDescription => shortDescription;

        public List<string> Tags => tags.ToList();

        public string SupportUrl => supportUrl;

        public string VanityUrl => "https://top.gg/bot/" + vanity;

        public string WebsiteUrl => websiteUrl;

        public BotReviews Reviews => reviews;
    }
}