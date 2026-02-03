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
                Token = Environment.GetEnvironmentVariable("TOPGG_TOKEN")
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
        public async Task TaskIsWeekendTestAsync()
        {
            await _api.IsWeekendAsync();
        }

        [Fact]
        public async Task RawUpdateCommandsAsync()
        {
            await _api.UpdateCommandsAsync("[{\"options\":[],\"name\":\"test\",\"name_localizations\":null,\"description\":\"command description\",\"description_localizations\":null,\"contexts\":[],\"default_permission\":null,\"default_member_permissions\":null,\"dm_permission\":false,\"integration_types\":[],\"nsfw\":false}]");
        }

        [Fact]
        public async Task GetVoteAsync()
        {
            await _api.GetVoteAsync(661200758510977084);
        }
    }
}