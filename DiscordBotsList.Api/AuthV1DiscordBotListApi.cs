#nullable enable

using System.Net.Http;
using System.Threading.Tasks;
using DiscordBotsList.Api.Internal;

namespace DiscordBotsList.Api
{
    public enum UserSource
    {
        Discord,
        Topgg,
    }

    public class AuthV1DiscordBotListApi : AuthDiscordBotListApi
    {
        public AuthV1DiscordBotListApi(string token) : base(token) {}
        public AuthV1DiscordBotListApi(ulong? initialSelfId, string token) : base(initialSelfId, token) {}

        /// <summary>
        ///     Updates the application commands list in your Discord bot's Top.gg page
        /// </summary>
        /// <typeparam name="T">Serializable list of Discord application commands</typeparam>
        /// <param name="commands">A list of application commands in raw Discord API JSON objects</param>
        public async Task UpdateBotCommandsAsync<T>(T commands)
        {
            await PostAsync<T, string>("/v1/projects/@me/commands", commands);
        }

        /// <summary>
        ///     Get the latest vote information of a Top.gg user on your project
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <param name="source">The ID type to use. Defaults to "Discord"</param>
        /// <returns>The user's latest vote information on your project or null if the user has not voted for your project in the past 12 hours</returns>
        public async Task<Vote?> GetVoteAsync(ulong id, UserSource source = UserSource.Discord)
        {
            try
            {
                return await GetAsync<Vote>($"/v1/projects/@me/votes/{id}?source={source.ToString().ToLower()}");
            }
            catch (HttpRequestException error)
            {
                if (error.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }
    }
}