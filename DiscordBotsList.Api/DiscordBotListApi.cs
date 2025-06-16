using DiscordBotsList.Api.Internal;
using DiscordBotsList.Api.Internal.Queries;
using DiscordBotsList.Api.Objects;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBotsList.Api
{
    public class DiscordBotListApi
    {
        protected const string baseEndpoint = "https://top.gg/api/";
        private readonly JsonSerializerOptions _serializerOptions;
        protected HttpClient _httpClient;

        public DiscordBotListApi()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new ULongToStringConverter());
        }

        /// <summary>
        ///     Gets bots from botlist
        /// </summary>
        /// <param name="sort">sorts results based on their monthly vote count ("montlyPoints"), id ("id"), or their submission date ("date")</param>
        /// <param name="count">amount of bots to retrieve (max: 500)</param>
        /// <param name="offset">amount of bots to skip</param>
        /// <returns>List of Bot Objects</returns>
        public async Task<ISearchResult<IDblBot>> GetBotsAsync(string sort = "monthlyPoints", int count = 50, int offset = 0)
        {
            if (sort != "monthlyPoints" && sort != "id" && sort != "date")
            {
                throw new ArgumentException("sort must be 'monthlyPoints', 'id', or 'date'.");
            }
            
            if (count < 0 || count > 500)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "count mustn't be negative or exceed 500.");
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "offset must not be negative.");
            }

            var result = await GetAsync<BotListQuery>($"bots?sort={sort}&limit=${count}&offset=${offset}");
            foreach (var bot in result.Items) (bot as Bot).api = this;
            return result;
        }

        /// <summary>
        ///     Get specific bot by Discord id
        /// </summary>
        /// <param name="id">Discord id</param>
        /// <returns>Bot Object</returns>
        public async Task<IDblBot> GetBotAsync(ulong id)
        {
            return (IDblBot)await GetBotAsync<Bot>(id);
        }

        /// <summary>
        ///     Get bot stats
        /// </summary>
        /// <returns>IBotStats object related to the bot</returns>
        public async Task<IDblBotStats> GetStatsAsync()
        {
            return await GetAsync<BotStatsObject>("bots/stats");
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
            var t = await GetAsync<T>($"bots/{id}");
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
        protected async Task<T> GetAsync<T>(string url)
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
            return (await GetAsync<WeekendObject>("weekend")).Weekend;
        }
    }
}