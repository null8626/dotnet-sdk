﻿using DiscordBotsList.Api.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Internal
{
    internal class SelfBot : Bot
    {
        public async Task<List<IDblEntity>> GetVotersAsync()
        {
            return await ((AuthDiscordBotListApi)api).GetVotersAsync();
        }

        public async Task<bool> HasVotedAsync(ulong userId)
        {
            return await ((AuthDiscordBotListApi)api).HasVoted(userId);
        }

        public async Task<bool> IsWeekendAsync()
        {
            return await ((AuthDiscordBotListApi)api).IsWeekendAsync();
        }

        public async Task UpdateStatsAsync(int guildCount)
        {
            await ((AuthDiscordBotListApi)api).UpdateStats(guildCount);
        }

        public async Task UpdateStatsAsync(int[] shards)
        {
            await ((AuthDiscordBotListApi)api).UpdateStats(0, shards.Length, shards);
        }

        public async Task UpdateStatsAsync(int shardCount, int totalShards, params int[] shards)
        {
            await ((AuthDiscordBotListApi)api).UpdateStats(shardCount, totalShards, shards);
        }
    }
}