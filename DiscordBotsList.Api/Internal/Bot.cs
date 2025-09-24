using DiscordBotsList.Api.Objects;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Internal
{
    public class Bot : Entity, IDblBot
    {
        internal DiscordBotListApi api;

        [JsonPropertyName("clientid")]
        [JsonConverter(typeof(ULongToStringConverter))]
        public ulong clientId { get; set; }

        [JsonPropertyName("prefix")] public string prefix { get; set; }

        [JsonPropertyName("shortdesc")] public string shortDescription { get; set; }

        [JsonPropertyName("longdesc")] public string longDescription { get; set; }

        [JsonPropertyName("tags")] public List<string> tags { get; set; }

        [JsonPropertyName("website")] public string websiteUrl { get; set; }

        [JsonPropertyName("support")]
        [Obsolete("Actually refers to the entire support invite URL, not just its invite code. Use SupportUrl instead.")]
        public string SupportInviteCode { get; set; }

        [JsonPropertyName("github")] public string githubUrl { get; set; }

        [JsonPropertyName("owners")] public List<ulong> owners { get; set; }

        [JsonPropertyName("invite")] public string customInvite { get; set; }

        [JsonPropertyName("date")]
        [Obsolete("Actually refers to when the bot was submitted. Use submittedAt instead.")]
        public DateTime approvedAt { get; set; }

        [JsonPropertyName("date")] public DateTime submittedAt { get; set; }

        [JsonPropertyName("certifiedBot")] public bool certified { get; set; }

        [JsonPropertyName("vanity")] public string vanity { get; set; }

        [JsonPropertyName("points")] public int points { get; set; }
        
        [JsonPropertyName("monthlyPoints")] public int monthlyPoints { get; set; }

        [JsonPropertyName("reviews")]
        public BotReviews reviews { get; set; }

        public ulong ClientId => clientId;

        public string VanityTag => vanity;

        [Obsolete("Actually refers to when the bot was submitted. Use SubmittedAt instead.")]
        public DateTime ApprovedAt => submittedAt;

        public DateTime SubmittedAt => submittedAt;

        public string GithubUrl => githubUrl;

        public string InviteUrl => customInvite ?? $"https://discord.com/oauth2/authorize?&client_id={Id}&scope=bot";

        public bool IsCertified => certified;

        public string LongDescription => longDescription;

        public string PrefixUsed => prefix;

        public List<ulong> OwnerIds => owners;

        public int Points => points;
        
        public int MonthlyPoints => monthlyPoints;

        public string ShortDescription => shortDescription;

        public List<string> Tags => tags;

#pragma warning disable CS0618
        public string SupportUrl => SupportInviteCode;
#pragma warning restore CS0618

        public string VanityUrl => "https://top.gg/bot/" + vanity;

        public string WebsiteUrl => websiteUrl;

        public BotReviews Reviews => reviews;

        public async Task<IDblBotStats> GetStatsAsync()
        {
            return await ((AuthDiscordBotListApi)api).GetBotStatsAsync(Id);
        }
    }
}