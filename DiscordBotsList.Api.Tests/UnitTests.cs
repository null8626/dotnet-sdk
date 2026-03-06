using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordBotsList.Api.Internal;
using DiscordBotsList.Api.Objects;
using Xunit;

namespace DiscordBotsList.Api.Tests
{
    public class UnitTests
    {
        public static IEnumerable<object[]> UserSources => Enum.GetValues<UserSource>().Select(source => new object[] { source });
        public static IEnumerable<object[]> ProjectTypes => Enum.GetValues<ProjectType>().Select(type => new object[] { type });

        private readonly DiscordBotListApi Client = new(new HttpClient(new Mocks()));

        [Fact]
        public async Task GetSelfAsync() => await Client.GetSelfAsync();

        [Fact]
        public async Task PostCommandsAsync() => await Client.PostCommandsAsync(Mocks.ReadJson("PostCommands"));

        [Theory]
        [MemberData(nameof(UserSources))]
        public async Task GetVoteAsync(UserSource source)
        {
            await Client.GetVoteAsync(661200758510977084, source);
        }

        [Fact]
        public async Task GetVotesAsync()
        {
            var firstPage = await Client.GetVotesAsync(DateTime.Now);
            await firstPage.Next();
        }

        [Theory]
        [MemberData(nameof(ProjectTypes))]
        public void Widgets(ProjectType type)
        {
            Widget.Large(type, 123456);
            Widget.Votes(type, 123456);
            Widget.Owner(type, 123456);
            Widget.Social(type, 123456);
        }
    }
}