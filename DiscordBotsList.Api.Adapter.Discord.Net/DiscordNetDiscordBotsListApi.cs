﻿using Discord;
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
    }

    public class DiscordNetDblApi : AuthDiscordBotListApi
    {
        protected IDiscordClient client;

        public DiscordNetDblApi(IDiscordClient client, string dblToken) : base(client.CurrentUser.Id, dblToken)
        {
            this.client = client;
        }

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
}