namespace DiscordBotsList.Api.Objects
{
    public static class Widget
    {
        /// <summary>
        ///     Generates a large widget URL.
        /// </summary>
        /// <param name="entityId">The entity ID.</param>
        /// <returns>The widget URL.</returns>
        public static string Large(ulong entityId)
        {
            return $"{DiscordBotListApi.baseEndpoint}/widgets/large/{entityId}";
        }
    }
}