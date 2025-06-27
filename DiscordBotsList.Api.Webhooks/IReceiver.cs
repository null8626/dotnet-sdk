using System.Threading.Tasks;

namespace DiscordBotsList.Api.Webhooks
{
    public interface IReceiver<T>
    {
        /// <summary>
        ///     Receives webhook data.
        /// </summary>
        /// <param name="data">The webhook data.</param>
        /// <returns>Return type can be anything as it's ignored.</returns>
        Task Callback(T data);
    }
}