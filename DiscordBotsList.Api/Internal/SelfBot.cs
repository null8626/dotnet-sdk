using DiscordBotsList.Api.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Internal
{
    internal class SelfBot : Bot, IDblSelfBot
    {
        public async Task<List<IDblEntity>> GetVotersAsync(int page = 1)
        {
            return await api.GetVotersAsync(page);
        }

        public async Task<bool> HasVotedAsync(ulong userId)
        {
            return await api.HasVoted(userId);
        }

        public async Task<bool> IsWeekendAsync()
        {
            return await api.IsWeekendAsync();
        }

        public async Task UpdateServerCountAsync(int serverCount)
        {
            await api.UpdateServerCountAsync(serverCount);
        }
    }
}