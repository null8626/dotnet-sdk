using System;
using System.Threading.Tasks;
using Xunit;

namespace DiscordBotsList.Api.Tests
{
    public class Credentials
    {
        public ulong BotId { get; set; }
        public string Token { get; set; }

        public static Credentials LoadFromEnv()
        {
            return new Credentials()
            {
                BotId = ulong.Parse(Environment.GetEnvironmentVariable("BOT_ID")),
                Token = Environment.GetEnvironmentVariable("API_KEY")
            };
        }
    }

    public class UnitTests
    {
        private readonly AuthDiscordBotListApi _api;
        private readonly Credentials _cred;

        public UnitTests()
        {
            _cred = Credentials.LoadFromEnv();
            _api = new AuthDiscordBotListApi(_cred.BotId, _cred.Token);
        }

        [Fact]
        public async Task HasVotedTestAsync()
        {
            Assert.False(await _api.HasVotedAsync(0));
        }

        [Fact]
        public async Task TaskIsWeekendTestAsync()
        {
            await _api.IsWeekendAsync();
        }

        [Fact]
        public async Task TaskGetVotersTestAsync()
        {
            Assert.NotNull(await _api.GetVotersAsync());
        }

        [Fact]
        public async Task GetBotTestAsync()
        {
            var botId = 1026525568344264724U;
            var bot = await _api.GetBotAsync(botId);
            Assert.NotNull(bot);
            Assert.Equal(botId, bot.Id);
        }

        [Fact]
        public async Task GetMeTestAsync()
        {
            Assert.NotNull(await _api.GetMeAsync());
        }

        [Fact]
        public async Task GetUsersGetStatsTest()
        {
            var bots = await _api.GetBotsAsync();

            Assert.NotNull(bots);
            Assert.NotEmpty(bots.Items);
        }
    }
}