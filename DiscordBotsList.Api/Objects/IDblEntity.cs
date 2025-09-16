namespace DiscordBotsList.Api.Objects
{
    public interface IDblEntity
    {
        /// <summary>
        ///     ID
        /// </summary>
        ulong Id { get; }

        /// <summary>
        ///     Username
        /// </summary>
        string Username { get; }

        /// <summary>
        ///     Discord avatar URL, or default avatar if none found.
        /// </summary>
        string AvatarUrl { get; }
    }
}