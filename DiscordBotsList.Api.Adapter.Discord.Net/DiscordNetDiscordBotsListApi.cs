using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Adapter.Discord.Net
{
    public static class DiscordNetDblUtils
    {
        public static DiscordNetDblApi CreateDblApi(this DiscordSocketClient client, string dblToken)
        {
            return new DiscordNetDblApi(client, dblToken);
        }
    }

    public class DiscordNetDblApi : AuthDiscordBotListApi
    {
        protected DiscordSocketClient client;

        public DiscordNetDblApi(DiscordSocketClient bot, string dblToken) : base(null, dblToken)
        {
            try
            {
                SelfId = bot.CurrentUser.Id;
            }
            catch (NullReferenceException)
            {
                bot.Ready += () =>
                {
                    SelfId = bot.CurrentUser.Id;

                    return Task.CompletedTask;
                };
            }

            client = bot;
        }

        /// <summary>
        ///     Creates a SubmissionAdapter that updates your servercount on RunAsync().
        /// </summary>
        /// <param name="updateTime">
        ///     Timespan for when you want to submit guildcount, must be at least 15 minutes
        /// </param>
        /// <returns>A SubmissionAdapter that updates your servercount on RunAsync().</returns>
        /// <seealso cref="ListenAsync()" />
        public SubmissionAdapter CreateListener(TimeSpan? updateTime = null)
        {
            return new SubmissionAdapter(this, client, updateTime ?? TimeSpan.FromMinutes(15));
        }
    }
}