namespace DiscordBotsList.Api.Webhooks
{
    /// <summary>
    ///     A `webhook.test` webhook payload.
    /// </summary>
    public class TestPayload
    {
        /// <summary>
        ///     The project that the test refers to.
        /// </summary>
        public PartialProject project { get; init; }
        
        /// <summary>
        ///     The user who triggered this test.
        /// </summary>
        public User user { get; init; }
    }
}