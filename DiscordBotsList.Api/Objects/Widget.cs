using System.Text.RegularExpressions;

namespace DiscordBotsList.Api.Objects
{
    public enum WidgetType
    {
        DiscordBot,
        DiscordServer,
    }

    public static partial class Widget
    {
        [GeneratedRegex("(?<!^)([A-Z])", RegexOptions.Compiled)]
        private static partial Regex typeConversionRegex();

        /// <summary>
        ///     Generates a large widget URL.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="projectId">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Large(WidgetType type, ulong projectId) => $"{DiscordBotListApi.baseEndpoint}/v1/widgets/large/{typeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{projectId}";

        /// <summary>
        ///     Generates a small widget URL for displaying votes.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="projectId">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Votes(WidgetType type, ulong projectId) => $"{DiscordBotListApi.baseEndpoint}/v1/widgets/small/votes/{typeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{projectId}";

        /// <summary>
        ///     Generates a small widget URL for displaying a project's owner.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="projectId">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Owner(WidgetType type, ulong projectId) => $"{DiscordBotListApi.baseEndpoint}/v1/widgets/small/owner/{typeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{projectId}";

        /// <summary>
        ///     Generates a small widget URL for displaying social stats.
        /// </summary>
        /// <param name="type">The widget type.</param>
        /// <param name="projectId">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Social(WidgetType type, ulong projectId) => $"{DiscordBotListApi.baseEndpoint}/v1/widgets/small/social/{typeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{projectId}";
    }
}