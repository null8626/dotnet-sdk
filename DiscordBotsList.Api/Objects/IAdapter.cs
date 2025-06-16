#nullable enable

using System;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Objects
{
    public interface IAdapter
    {
        event Action<Exception?> Posted;

        Task RunAsync();

        bool IsRunning();

        void Start();

        void Stop();

        Task StopAsync();
    }
}