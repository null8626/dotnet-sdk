using System.Text.RegularExpressions;

namespace DiscordBotsList.Api.Objects
{
    public enum WidgetType
    {
        DiscordBot,
        DiscordServer,
    }

    public static class Widget
    {
        private static readonly Regex typeConversionRegex = new Regex("(?<!^)([A-Z])", RegexOptions.Compiled);

        /// <summary>
        ///     Generates a large widget URL.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Large(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/large/{typeConversionRegex.Replace(type.ToString(), "/$1").ToLower()}/{entityId}";
        }

        /// <summary>
        ///     Generates a small widget URL for displaying votes.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Votes(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/small/votes/{typeConversionRegex.Replace(type.ToString(), "/$1").ToLower()}/{entityId}";
        }

        /// <summary>
        ///     Generates a small widget URL for displaying an entity's owner.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Owner(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/small/owner/{typeConversionRegex.Replace(type.ToString(), "/$1").ToLower()}/{entityId}";
        }

        /// <summary>
        ///     Generates a small widget URL for displaying social stats.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Social(WidgetType type, ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/small/social/{typeConversionRegex.Replace(type.ToString(), "/$1").ToLower()}/{entityId}";
        }
    }
}