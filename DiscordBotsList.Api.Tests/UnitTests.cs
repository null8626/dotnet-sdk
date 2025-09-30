using System;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordBotsList.Api.Adapter.Discord.Net;
using Xunit;

namespace DiscordBotsList.Api.Tests
{
    public class Credentials
    {
        public ulong BotId { get; set; }
        public string BotToken { get; set; }
        public string TopggToken { get; set; }

        public static Credentials LoadFromEnv()
        {
            return new Credentials()
            {
                BotId = ulong.Parse(Environment.GetEnvironmentVariable("BOT_ID")),
                BotToken = Environment.GetEnvironmentVariable("BOT_TOKEN"),
                TopggToken = Environment.GetEnvironmentVariable("TOPGG_TOKEN")
            };
        }
    }

    public class UnitTests : IDisposable
    {
        private readonly Credentials _cred;
        private readonly DiscordSocketClient _bot;
        private readonly DiscordNetDblApi _api;

        public UnitTests()
        {
            Environment.SetEnvironmentVariable("XUNIT_TEST", "true");

            _cred = Credentials.LoadFromEnv();
            _bot = new DiscordSocketClient();
            _api = new DiscordNetDblApi(_bot, _cred.TopggToken)
            {
                SelfId = _cred.BotId
            };
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

        [Fact]
        public async Task DiscordNetAdapterTest()
        {
            var postCounter = 0;
            var maxPosts = 3;

            var cancellationTokenSource = new CancellationTokenSource();

            _bot.Ready += () =>
            {
                Console.WriteLine("[DiscordNetAdapterTest]: Bot has started!");

                var adapter = _api.CreateListener(TimeSpan.FromSeconds(2));

                adapter.Log += (message) =>
                {
                    Console.WriteLine($"[DiscordNetAdapter]: {message}");

                    postCounter++;

                    if (postCounter == maxPosts)
                    {
                        adapter.Stop();
                        cancellationTokenSource.Cancel();
                    }
                };

                adapter.Start();

                return Task.CompletedTask;
            };

            await _bot.LoginAsync(Discord.TokenType.Bot, _cred.BotToken);
            await _bot.StartAsync();

            try
            {
                await Task.Delay(11000, cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
                return;
            }
            finally
            {
                await _bot.LogoutAsync();
                await _bot.StopAsync();
            }

            Assert.Fail($"Unable to post bot stats automatically {maxPosts} times.");
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable("XUNIT_TEST", null);
        }
    }
}