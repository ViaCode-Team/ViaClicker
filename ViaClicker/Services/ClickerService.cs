using MouseSimLib;

namespace ViaClicker.Services
{
    public class ClickerService(ConfigClicker config)
    {
        private CancellationTokenSource? _cts;
        private readonly ConfigClicker _config = config;
        private readonly object _lock = new(); // для синхранизации потоко-безопастности

        public bool IsRunning { get; private set; }

        public void Start()
        {
            lock (_lock)
            {
                if (IsRunning)
                    return;

                _cts = new CancellationTokenSource();
                IsRunning = true;
                Task.Run(() => LoopAsync(_cts.Token));
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (!IsRunning)
                    return;

                _cts?.Cancel();
                _cts = null;
                IsRunning = false;
            }
        }

        private async Task LoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    MouseSimulator.Click(_config.ClickButton);
                    await Task.Delay(_config.IntervalMs, token);
                }
            }
            catch (TaskCanceledException) { }
        }
    }
}
