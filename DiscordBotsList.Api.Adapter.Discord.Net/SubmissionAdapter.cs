using Discord;
using System;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Adapter.Discord.Net
{
    public class SubmissionAdapter(AuthDiscordBotListApi api, IDiscordClient client, TimeSpan updateTime) : Internal.Adapter(updateTime)
    {
        private readonly AuthDiscordBotListApi api = api;
        private readonly IDiscordClient client = client;

        public override async Task RunAsync()
        {
            await api.UpdateStats((await client.GetGuildsAsync()).Count);
        }
    }
}