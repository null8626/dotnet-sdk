using System;
using System.Threading.Tasks;
using DiscordBotsList.Api.Internal;
using Xunit;

namespace DiscordBotsList.Api.Tests
{
    public class UnitTests
    {
        private readonly DiscordBotListApi Client;

        public UnitTests() => Client = new DiscordBotListApi(Environment.GetEnvironmentVariable("TOPGG_TOKEN"));

        [Fact]
        public async Task GetSelfAsync() => await Client.GetSelfAsync();

        [Fact]
        public async Task PostCommandsAsync() => await Client.PostCommandsAsync("[{\"options\":[],\"name\":\"test\",\"name_localizations\":null,\"description\":\"command description\",\"description_localizations\":null,\"contexts\":[],\"default_permission\":null,\"default_member_permissions\":null,\"dm_permission\":false,\"integration_types\":[],\"nsfw\":false}]");

        [Fact]
        public async Task GetVoteAsync()
        {
            await Client.GetVoteAsync(661200758510977084, UserSource.Discord);
            await Client.GetVoteAsync(8226924471638491136, UserSource.Topgg);
        }

        [Fact]
        public async Task GetVotesAsync()
        {
            var firstPage = await Client.GetVotesAsync(DateTime.Now);
            await firstPage.Next();
        }
    }
}