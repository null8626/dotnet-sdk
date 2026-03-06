using System;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordBotsList.Api.Internal;
using Xunit;

namespace DiscordBotsList.Api.Tests
{
    public class UnitTests
    {
        private readonly DiscordBotListApi Client = new(new HttpClient(new Mocks()));

        [Fact]
        public async Task GetSelfAsync() => await Client.GetSelfAsync();

        [Fact]
        public async Task PostCommandsAsync() => await Client.PostCommandsAsync(Mocks.ReadJson("PostCommands"));

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