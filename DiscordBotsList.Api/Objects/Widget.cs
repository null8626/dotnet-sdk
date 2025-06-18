namespace DiscordBotsList.Api.Objects
{
    public enum WidgetType
    {
        DISCORD_BOT,
        DISCORD_SERVER,
    }

    public static class Widget
    {
        /// <summary>
        ///     Generates a large widget URL.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Large(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/large/{type.ToString().ToLower().Replace('_', '/')}/{entityId}";
        }

        /// <summary>
        ///     Generates a small widget URL for displaying votes.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Votes(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/small/votes/{type.ToString().ToLower().Replace('_', '/')}/{entityId}";
        }

        /// <summary>
        ///     Generates a small widget URL for displaying an entity's owner.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Owner(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/small/owner/{type.ToString().ToLower().Replace('_', '/')}/{entityId}";
        }

        /// <summary>
        ///     Generates a small widget URL for displaying social stats.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Social(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/small/social/{type.ToString().ToLower().Replace('_', '/')}/{entityId}";
        }
    }
}