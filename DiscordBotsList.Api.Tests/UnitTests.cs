using System;
using System.Linq;
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
                Token = Environment.GetEnvironmentVariable("API_KEY"),
            };
        }
    }

    public class UnitTests
    {
        private readonly DiscordBotListApi _api;
        private readonly Credentials _cred;

        public UnitTests()
        {
            _cred = Credentials.LoadFromEnv();
            _api = new DiscordBotListApi(_cred.BotId, _cred.Token);
        }

        [Fact]
        public async Task HasVotedTestAsync()
        {
            Assert.False(await _api.HasVoted(0));
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
            var botId = 264811613708746752U;
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
        public async Task GetBotsTestAsync()
        {
            var bots = await _api.GetBotsAsync();

            Assert.NotNull(bots);
            Assert.NotEmpty(bots.Items);

            var firstBot = bots.Items.First();

            Assert.NotNull(firstBot);
        }

        [Fact]
        public async Task GetStatsTestAsync()
        {
            Assert.NotNull(await _api.GetStatsAsync());
        }
        
        [Fact]
        public async Task UpdateStatsTestAsync()
        {
            await _api.UpdateStatsAsync(2);
        }
    }
}