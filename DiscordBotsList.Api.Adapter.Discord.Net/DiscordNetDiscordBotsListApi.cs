﻿using Discord;
using Discord.WebSocket;
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

    public class DiscordNetDblApi : DiscordBotListApi
    {
        protected IDiscordClient client;

        public DiscordNetDblApi(IDiscordClient client, string dblToken) : base(client.CurrentUser.Id, dblToken)
        {
            this.client = client;
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