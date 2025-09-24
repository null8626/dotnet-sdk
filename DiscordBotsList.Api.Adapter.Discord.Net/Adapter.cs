#nullable enable

using DiscordBotsList.Api.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Adapter.Discord.Net
{
    public class Adapter : IAdapter
    {
        public event Action<string> Log = _ => { };
        private readonly TimeSpan updateTime;
        private Timer? timer;

        public Adapter(TimeSpan updateTime)
        {
            if (updateTime < TimeSpan.FromMinutes(15))
            {
                updateTime = TimeSpan.FromMinutes(15);
            }

            this.updateTime = updateTime;
        }

        public virtual Task RunAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsRunning() => timer != null;

        private async void Autopost(object? state)
        {
            try
            {
                await RunAsync();

                Log?.Invoke("Just automatically posted bot stats.");
            }
            catch (Exception ex)
            {
                Stop();

                Log?.Invoke("Unable to automatically post bot stats: " + ex.Message);
            }
        }

        public void Start()
        {
            if (!IsRunning())
            {
                timer = new Timer(Autopost, null, TimeSpan.Zero, updateTime);
            }
        }

        public void Stop()
        {
            timer?.Dispose();
            timer = null;
        }
    }
}