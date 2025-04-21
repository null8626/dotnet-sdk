namespace DiscordBotsList.Api.Objects
{
    public interface IDblUser : IDblEntity
    {
        string Biography { get; }

        SocialConnections Connections { get; }

        string Color { get; }

        bool IsSupporter { get; }

        bool IsModerator { get; }

        bool IsWebModerator { get; }

        bool IsAdmin { get; }
    }
}