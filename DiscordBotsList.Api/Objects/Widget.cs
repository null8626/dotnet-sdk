using System.Text.RegularExpressions;
using DiscordBotsList.Api.Internal;

namespace DiscordBotsList.Api.Objects
{
    public static partial class Widget
    {
        [GeneratedRegex("(?<!^)([A-Z])", RegexOptions.Compiled)]
        private static partial Regex TypeConversionRegex();

        /// <summary>
        ///     Generates a large widget URL.
        /// </summary>
        /// <param name="type">The project's type.</param>
        /// <param name="id">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Large(ProjectType type, ulong id) => $"{DiscordBotListApi.BaseURL}/widgets/large/{TypeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{id}";

        /// <summary>
        ///     Generates a small widget URL for displaying votes.
        /// </summary>
        /// <param name="type">The project's type.</param>
        /// <param name="id">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Votes(ProjectType type, ulong id) => $"{DiscordBotListApi.BaseURL}/widgets/small/votes/{TypeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{id}";

        /// <summary>
        ///     Generates a small widget URL for displaying a project's owner.
        /// </summary>
        /// <param name="type">The project's type.</param>
        /// <param name="id">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Owner(ProjectType type, ulong id) => $"{DiscordBotListApi.BaseURL}/widgets/small/owner/{TypeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{id}";

        /// <summary>
        ///     Generates a small widget URL for displaying social stats.
        /// </summary>
        /// <param name="type">The project's type.</param>
        /// <param name="id">The project ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Social(ProjectType type, ulong id) => $"{DiscordBotListApi.BaseURL}/widgets/small/social/{TypeConversionRegex().Replace(type.ToString(), "/$1").ToLower()}/{id}";
    }
}