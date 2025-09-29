namespace DiscordBotsList.Api.Objects
{
    public interface IDblEntity
    {
        /// <summary>
        ///     Id
        /// </summary>
        ulong Id { get; }

        /// <summary>
        ///     Username
        /// </summary>
        string Username { get; }

        /// <summary>
        ///     Discriminator, the XXXX#1234 part
        /// </summary>
        string Discriminator { get; }

        /// <summary>
        ///     Avatar url, or default avatar if none found.
        /// </summary>
        string AvatarUrl { get; }
    }
}