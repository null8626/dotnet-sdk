using Discord;
using DiscordBotsList.Api.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Adapter.Discord.Net
{
    public class SubmissionAdapter : Adapter
    {
        private readonly DiscordBotListApi api;
        private readonly IDiscordClient client;

        public SubmissionAdapter(DiscordBotListApi api, IDiscordClient client, TimeSpan updateTime) : base(updateTime)
        {
            this.api = api;
            this.client = client;
        }

        public override async Task RunAsync()
        {
            await api.UpdateStatsAsync((await client.GetGuildsAsync()).Count);
        }
    }
}