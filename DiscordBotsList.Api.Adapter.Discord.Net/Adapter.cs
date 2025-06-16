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
                updateTime = TimeSpan.FromMinutes(15);
            }

            this.updateTime = updateTime;
            cancellationTokenSource = null;
        }

        public virtual async Task RunAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsRunning()
        {
            return cancellationTokenSource != null;
        }

        public void Start()
        {
            if (IsRunning())
            {
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        await RunAsync();
                    }
                    catch
                    {
                        cancellationTokenSource.Cancel();
                        cancellationTokenSource = null;
                        break;
                    }

                    SendLog("Submitted stats to Top.gg!");

                    await Task.Delay(updateTime, cancellationTokenSource.Token);
                }
            }, cancellationTokenSource.Token);
        }

        public void Stop()
        {
            if (IsRunning())
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
            }
        }

        public async Task StopAsync()
        {
            if (IsRunning())
            {
                await cancellationTokenSource.CancelAsync();
                cancellationTokenSource = null;
            }
        }

        private void SendLog(string msg)
        {
            Log?.Invoke(msg);
        }
    }
}