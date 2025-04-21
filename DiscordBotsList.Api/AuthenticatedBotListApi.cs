using DiscordBotsList.Api.Internal;
using DiscordBotsList.Api.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBotsList.Api
{
    public class AuthDiscordBotListApi : DiscordBotListApi
    {
        private readonly ulong _selfId;
        private readonly string _token;

        public AuthDiscordBotListApi(ulong selfId, string token)
        {
            _selfId = selfId;
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        /// <summary>
        ///     Gets your own bot with as an ISelfBot
        /// </summary>
        /// <returns>your own bot with as an ISelfBot</returns>
        public async Task<IDblSelfBot> GetMeAsync()
        {
            var bot = await GetBotAsync<SelfBot>(_selfId);
            bot.api = this;
            return (IDblSelfBot)bot;
        }

        /// <summary>
        ///     Gets unique voters that have voted on your bot
        ///     Max 1000, If you have more, you MUST use WEBHOOKS instead.
        /// </summary>
        /// <param name="page">The page number, defaults to 1</param>
        /// <returns>A list of voters</returns>
        public async Task<List<IDblEntity>> GetVotersAsync(int page = 1)
        {
            return (await GetVotersAsync<Entity>(page)).Cast<IDblEntity>().ToList();
        }

        /// <summary>
        ///     Update your stats
        /// </summary>
        /// <param name="guildCount">count of guilds</param>
        public async Task UpdateStats(int guildCount)
        {
            if (guildCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(guildCount), "guildCount cannot be less than 1.");
            }

            await UpdateStatsAsync(new GuildCountObject(guildCount));
        }

        /// <summary>
        ///     returns true if user have voted for the past 12 hours
        /// </summary>
        /// <param name="userId">Amount of days to filter</param>
        /// <returns>True or False</returns>
        public async Task<bool> HasVoted(ulong userId)
        {
            return await HasVotedAsync(userId);
        }

        protected async Task<List<T>> GetVotersAsync<T>(int page)
        {
            if (page < 1)
            {
                page = 1;
            }

            return await GetAuthorizedAsync<List<T>>(Utils.CreateQuery($"bots/votes?page={page}"));
        }

        protected async Task UpdateStatsAsync(object statsObject)
        {
            var json = JsonSerializer.Serialize(statsObject);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient
                .PostAsync($"{baseEndpoint}/bots/stats", httpContent);
        }

        protected async Task<T> GetAuthorizedAsync<T>(string url)
        {
            return await GetAsync<T>(url);
        }

        protected async Task<bool> HasVotedAsync(ulong userId)
        {
            var url = $"bots/check?userId={userId}";
            return (await GetAsync<HasVotedObject>(url)).HasVoted.GetValueOrDefault(0) == 1;
        }
    }
}