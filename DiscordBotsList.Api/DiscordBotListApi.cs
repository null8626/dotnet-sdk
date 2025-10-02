using DiscordBotsList.Api.Internal;
using DiscordBotsList.Api.Objects;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DiscordBotsList.Api
{
    public class DiscordBotListApi
    {
        internal static string baseEndpoint = "https://top.gg/api";
        private readonly JsonSerializerOptions _serializerOptions;
        protected readonly HttpClient _httpClient;

        public DiscordBotListApi()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new ULongToStringConverter());
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            if (typeof(T) == typeof(string))
            {
                return (T)(object)await response.Content.ReadAsStringAsync();
            }
            else if (response.Content.Headers.ContentType?.MediaType?.Contains("json") ?? false)
            {
                return await response.Content.ReadFromJsonAsync<T>(_serializerOptions);
            }

            return default;
        }

        /// <summary>
        ///     Performs a GET request
        /// </summary>
        /// <typeparam name="T">Type to parse to</typeparam>
        /// <param name="url">Url to get from</param>
        /// <returns>Object of type T</returns>
        protected async Task<T> GetAsync<T>(string url)
        {
            return await ProcessResponse<T>(await _httpClient.GetAsync(baseEndpoint + url));
        }

        /// <summary>
        ///     Performs a POST request
        /// </summary>
        /// <typeparam name="B">Serializable request body type. If this is a string, this is treated as a raw JSON string</typeparam>
        /// <typeparam name="T">Type to parse to</typeparam>
        /// <param name="url">Url to post from</param>
        /// <param name="body">The request body</param>
        /// <returns>Object of type T</returns>
        protected async Task<T> PostAsync<B, T>(string url, B body)
        {
            StringContent httpContent;

            if (typeof(B) == typeof(string))
            {
                httpContent = new StringContent((string)(object)body, Encoding.UTF8, "application/json");
            }
            else
            {
                var json = JsonSerializer.Serialize(body);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return await ProcessResponse<T>(await _httpClient.PostAsync(baseEndpoint + url, httpContent));
        }

        /// <summary>
        ///     Get bots
        /// </summary>
        /// <param name="count">amount of bots to appear per page (max: 500)</param>
        /// <param name="page">current page to query</param>
        /// <returns>List of Bot Objects</returns>
        [Obsolete("This method requires a token to work. Please use the AuthenticatedBotListApi class instead.", true)]
        public Task<ISearchResult<IDblBot>> GetBotsAsync(int count = 50, int page = 0)
        {
            return null;
        }

        /// <summary>
        ///     Get specific bot by Discord id
        /// </summary>
        /// <param name="id">Discord id</param>
        /// <returns>Bot Object</returns>
        [Obsolete("This method requires a token to work. Please use the AuthenticatedBotListApi class instead.", true)]
        public Task<IDblBot> GetBotAsync(ulong id)
        {
            return null;
        }

        /// <summary>
        ///     Get bot stats
        /// </summary>
        /// <param name="id">Discord id</param>
        /// <returns>IBotStats object related to the bot</returns>
        [Obsolete("This method requires a token to work. Please use the AuthenticatedBotListApi class instead.", true)]
        public Task<IDblBotStats> GetBotStatsAsync(ulong id)
        {
            return null;
        }

        /// <summary>
        ///     Get specific user by Discord id
        /// </summary>
        /// <param name="id">Discord id</param>
        /// <returns>User Object</returns>
        [Obsolete("This method requires a token to work. Please use the AuthenticatedBotListApi class instead.", true)]
        public Task<IDblUser> GetUserAsync(ulong id)
        {
            return null;
        }

        /// <summary>
        ///     returns true if voting multiplier = x2
        /// </summary>
        /// <returns>True or False</returns>
        public async Task<bool> IsWeekendAsync()
        {
            return (await GetAsync<WeekendObject>("/weekend")).Weekend;
        }
    }
}