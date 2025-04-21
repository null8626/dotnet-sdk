﻿namespace DiscordBotsList.Api.Objects
{
    public interface IDblEntity
    {
        /// <summary>
        ///     Discord Id
        /// </summary>
        ulong Id { get; }

        /// <summary>
        ///     Username of the entity
        /// </summary>
        string Username { get; }

        /// <summary>
        ///     Discord avatar url, or default avatar if none found.
        /// </summary>
        string AvatarUrl { get; }
    }
}