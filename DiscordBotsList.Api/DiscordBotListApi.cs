using DiscordBotsList.Api.Internal;
using DiscordBotsList.Api.Internal.Queries;
using DiscordBotsList.Api.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBotsList.Api
{
    public enum SortBotsBy
    {
        MonthlyPoints,
        Id,
        Date,
    }

    public class DiscordBotListApi
    {
        internal const string baseEndpoint = "https://top.gg/api/v1";
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ulong _selfId;
        private readonly HttpClient _httpClient;

        public DiscordBotListApi(ulong selfId, string token)
        {
            _selfId = selfId;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new ULongToStringConverter());
        }

        /// <summary>
        ///     Gets bots from botlist
        /// </summary>
        /// <param name="sortBy">sorts results based on their monthly vote count, id, or their submission date</param>
        /// <param name="count">amount of bots to retrieve (max: 500)</param>
        /// <param name="offset">amount of bots to skip</param>
        /// <returns>List of Bot Objects</returns>
        public async Task<ISearchResult<IDblBot>> GetBotsAsync(SortBotsBy sortBy = SortBotsBy.MonthlyPoints, int count = 50, int offset = 0)
        {
            if (count < 0 || count > 500)
            {
                count = 50;
            }

            if (offset < 0)
            {
                offset = 0;
            }

            var sortByString = sortBy.ToString();
            var result = await GetAsync<BotListQuery>($"/bots?sort={char.ToLowerInvariant(sortByString[0]) + sortByString.Substring(1)}&limit=${count}&offset=${offset}");

            foreach (var bot in result.Items) (bot as Bot).api = this;

            return result;
        }

        /// <summary>
        ///     Gets specific bot by Discord id
        /// </summary>
        /// <param name="id">Discord id</param>
        /// <returns>Bot Object</returns>
        public async Task<IDblBot> GetBotAsync(ulong id)
        {
            return await GetBotAsync<Bot>(id);
        }

        /// <summary>
        ///     Gets your bot's server count
        /// </summary>
        /// <returns>Your bot's server count if available</returns>
        public async Task<int> GetServerCountAsync()
        {
            var result = await GetAsync<BotStatsObject>("/bots/stats");

            return result.ServerCount;
        }

        /// <summary>
        ///     Template
        ///     of GetBotAsync for internal usage.
        /// </summary>
        /// <typeparam name="T">Type of Bot</typeparam>
        /// <param name="id">Discord id</param>
        /// <returns>Bot object of type T</returns>
        internal async Task<T> GetBotAsync<T>(ulong id) where T : Bot
        {
            var t = await GetAsync<T>($"/bots/{id}");
            if (t == null) return null;
            t.api = this;
            return t;
        }

        /// <summary>
        ///     Gets and parses objects
        /// </summary>
        /// <typeparam name="T">Type to parse to</typeparam>
        /// <param name="url">Url to get from</param>
        /// <returns>Object of type T</returns>
        private async Task<T> GetAsync<T>(string url)
        {
            var t = await _httpClient.GetAsync(baseEndpoint + url);
            var result = t.IsSuccessStatusCode
            ? ApiResult<T>.FromSuccess(await t.Content.ReadFromJsonAsync<T>(_serializerOptions))
            : ApiResult<T>.FromHttpError(t.StatusCode);
            return result.Value;
        }

        /// <summary>
        ///     returns true if voting multiplier = x2
        /// </summary>
        /// <returns>True or False</returns>
        public async Task<bool> IsWeekendAsync()
        {
            return (await GetAsync<WeekendObject>("/weekend")).Weekend;
        }

        /// <summary>
        ///     Gets unique voters that have voted on your bot
        ///     Max 1000, If you have more, you MUST use WEBHOOKS instead.
        /// </summary>
        /// <param name="page">The page number, defaults to 1</param>
        /// <returns>A list of voters</returns>
        public async Task<List<IDblEntity>> GetVotersAsync(int page = 1)
        {
            return (await GetAsync<List<Entity>>($"/bots/{_selfId}/votes?page={Math.Max(page, 1)}")).Cast<IDblEntity>().ToList();
        }

        /// <summary>
        ///     Updates your bot's server count
        /// </summary>
        /// <param name="serverCount">Your bot's server count</param>
        public async Task UpdateServerCountAsync(int serverCount)
        {
            if (serverCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(serverCount), "serverCount cannot be less than 1.");
            }

            var json = JsonSerializer.Serialize(new ServerCountObject(serverCount));
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync($"{baseEndpoint}/bots/stats", httpContent);
        }

        /// <summary>
        ///     returns true if user have voted for the past 12 hours
        /// </summary>
        /// <param name="userId">Amount of days to filter</param>
        /// <returns>True or False</returns>
        public async Task<bool> HasVoted(ulong userId)
        {
            return (await GetAsync<HasVotedObject>($"/bots/check?userId={userId}")).HasVoted.GetValueOrDefault(0) == 1;
        }
    }
}