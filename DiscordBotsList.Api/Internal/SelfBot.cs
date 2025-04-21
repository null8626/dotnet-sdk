using DiscordBotsList.Api.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Internal
{
    internal class SelfBot : Bot
    {
        public async Task<List<IDblEntity>> GetVotersAsync(int page = 1)
        {
            return await ((AuthDiscordBotListApi)api).GetVotersAsync(page);
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
    }
}