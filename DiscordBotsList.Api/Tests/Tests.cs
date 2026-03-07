using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordBotsList.Api.Data;
using Xunit;

namespace DiscordBotsList.Api.Tests
{
    public class Tests
    {
        public static IEnumerable<TheoryDataRow<UserSource>> UserSources => Enum.GetValues<UserSource>().Select(source => new TheoryDataRow<UserSource>(source));
        public static IEnumerable<TheoryDataRow<ProjectType>> ProjectTypes => Enum.GetValues<ProjectType>().Select(payload => new TheoryDataRow<ProjectType>(payload));

        private readonly DiscordBotListApi Client = new(new HttpClient(new Mock()));

        [Fact]
        public async Task GetSelfAsync() => await Client.GetSelfAsync();

        [Fact]
        public async Task PostCommandsAsync() => await Client.PostCommandsAsync(Mock.ReadJson("PostCommands"));

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