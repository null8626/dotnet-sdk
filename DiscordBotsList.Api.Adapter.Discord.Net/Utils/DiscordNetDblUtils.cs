﻿using Discord.WebSocket;

namespace DiscordBotsList.Api.Adapter.Discord.Net.Utils
{
    public static class DiscordNetDblUtils
    {
        /// <summary>
        ///     Creates a DiscordBotsList Api
        /// </summary>
        /// <param name="client">your client</param>
        /// <param name="dblToken">Your DiscordBotsList token</param>
        /// <returns>A new instance of a DblApi</returns>
        public static DiscordNetDblApi CreateDblApi(this DiscordSocketClient client, string dblToken)
        {
            return new DiscordNetDblApi(client, dblToken);
        }
    }
}