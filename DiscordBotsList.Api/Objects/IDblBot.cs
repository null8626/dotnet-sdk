﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Objects
{
    public interface IDblBot : IDblEntity
    {
        string PrefixUsed { get; }

        string ShortDescription { get; }

        string LongDescription { get; }

        List<string> Tags { get; }

        string WebsiteUrl { get; }

        string SupportUrl { get; }

        string GithubUrl { get; }

        List<ulong> OwnerIds { get; }

        string InviteUrl { get; }

        DateTime SubmittedAt { get; }

        string VanityUrl { get; }

        int Points { get; }

        int MonthlyPoints { get; }

        BotReviews Reviews { get; }
    }

    public interface IDblSelfBot : IDblBot
    {
        Task<List<IDblEntity>> GetVotersAsync(int page);

        Task<bool> HasVotedAsync(ulong userId);

        Task<bool> IsWeekendAsync();

        Task UpdateServerCountAsync(int serverCount);
    }
}