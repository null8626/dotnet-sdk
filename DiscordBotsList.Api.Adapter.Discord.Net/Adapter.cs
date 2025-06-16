using DiscordBotsList.Api.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Adapter.Discord.Net
{
    public class Adapter : IAdapter
    {
        public event Action<string> Log;

        private readonly TimeSpan updateTime;

        private CancellationTokenSource cancellationTokenSource;

        public Adapter(TimeSpan updateTime)
        {
            if (updateTime < TimeSpan.FromMinutes(15))
            {
                throw new ArgumentException("updateTime must be at least 15 minutes.", nameof(updateTime));
            }

            this.updateTime = updateTime;
            cancellationTokenSource = null;
        }

        public virtual async Task RunAsync()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            if (cancellationTokenSource != null)
            {
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await RunAsync();

                    SendLog("Submitted stats to Top.gg!");
                    
                    await Task.Delay(updateTime, cancellationTokenSource.Token);
                }
            }, cancellationTokenSource.Token);
        }

        public void Stop()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
            }
        }

        private void SendLog(string msg)
        {
            Log?.Invoke(msg);
        }
    }
}