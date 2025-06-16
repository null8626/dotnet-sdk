using System;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Objects
{
    public interface IAdapter
    {
        event Action<string> Log;

        Task RunAsync();

        bool IsRunning();

        void Start();

        void Stop();
    }
}