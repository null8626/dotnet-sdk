#nullable enable

using DiscordBotsList.Api.Objects;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotsList.Api.Adapter.Discord.Net
{
    public class Adapter : IAdapter
    {
        public event Action<Exception?> Posted = _ => { };
        private readonly TimeSpan updateTime;
        private readonly BackgroundWorker backgroundWorker;

        public Adapter(TimeSpan updateTime)
        {
            if (updateTime < TimeSpan.FromMinutes(15))
            {
                updateTime = TimeSpan.FromMinutes(15);
            }

            this.updateTime = updateTime;

            backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            backgroundWorker.DoWork += Autopost;
        }

        public virtual Task RunAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsRunning()
        {
            return backgroundWorker.IsBusy;
        }

        private void Autopost(object? sender, DoWorkEventArgs e)
        {
            while (!backgroundWorker.CancellationPending)
            {
                try
                {
                    RunAsync().GetAwaiter().GetResult();
                }
                catch (Exception err)
                {
                    Stop();

                    Posted?.Invoke(err);
                    break;
                }

                Posted?.Invoke(null);

                Thread.Sleep(updateTime);
            }
        }

        public void Start()
        {
            if (!IsRunning())
            {
                backgroundWorker.RunWorkerAsync();
            }
        }

        public void Stop()
        {
            if (IsRunning())
            {
                backgroundWorker.CancelAsync();
            }
        }
    }
}