using Discord;
using Discord.WebSocket;
using DiscordBotsList.Api.Objects;
using System;

namespace DiscordBotsList.Api.Adapter.Discord.Net
{
    public static class DiscordNetDblUtils
    {
        public static DiscordNetDblApi CreateDblApi(this DiscordSocketClient client, string dblToken)
        {
            return new DiscordNetDblApi(client, dblToken);
        }

        public static ShardedDiscordNetDblApi CreateDblApi(this DiscordShardedClient client, string dblToken)
        {
            return new ShardedDiscordNetDblApi(client, dblToken);
        }
    }

    public class DiscordNetDblApi(IDiscordClient client, string dblToken) : AuthDiscordBotListApi(client.CurrentUser.Id, dblToken)
    {
        private readonly IDiscordClient client = client;

        /// <summary>
        ///     Creates an IAdapter that updates your servercount on RunAsync().
        /// </summary>
        /// <param name="client">Your already connected client</param>
        /// <param name="updateTime">
        ///     Timespan for when you want to submit guildcount, leave null if you want it every JoinedGuild
        ///     event
        /// </param>
        /// <returns>an IAdapter that updates your servercount on RunAsync(), does not automatically do it yet.</returns>
        /// <seealso cref="ListenAsync()" />
        public virtual IAdapter CreateListener(TimeSpan? updateTime = null)
        {
            return new SubmissionAdapter(this, client, updateTime ?? TimeSpan.Zero);
        }
    }

    public class ShardedDiscordNetDblApi(DiscordShardedClient client, string dblToken) : DiscordNetDblApi(client, dblToken)
    {
        /// <summary>
        ///     Creates an IAdapter that updates your servercount on RunAsync().
        /// </summary>
        /// <param name="client">Your already connected client</param>
        /// <param name="updateTime">
        ///     Timespan for when you want to submit guildcount, leave null if you want it every JoinedGuild
        ///     event
        /// </param>
        /// <returns>an IAdapter that updates your servercount on RunAsync(), does not automatically do it yet.</returns>
        /// <seealso cref="ListenAsync()" />
        public override IAdapter CreateListener(TimeSpan? updateTime = null)
        {
            return new ShardedSubmissionAdapter(this, client, updateTime ?? TimeSpan.Zero);
        }
    }
}