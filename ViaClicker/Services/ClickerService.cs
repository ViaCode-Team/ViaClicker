using System;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Events;

namespace ViaClicker
{
    public class ClickerService
    {
        private CancellationTokenSource? cts;
        private readonly ConfigClicker config;

        public bool IsRunning { get; private set; }

        public ClickerService()
        {
            config = ClickerConfigService.Load();
        }

        public void Start()
        {
            if (IsRunning)
                return;

            cts = new CancellationTokenSource();
            IsRunning = true;
            Task.Run(() => Loop(cts.Token));
        }

        public void Pause()
        {
            if (!IsRunning)
                return;

            cts!.Cancel();
            IsRunning = false;
        }

        private async Task Loop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Click();
                await Task.Delay(config.IntervalMs, token);
            }
        }

        private Task Click()
        {

            switch (config.ClickButton)
            {
                case MouseButton.Left:
                    return Simulate.Events().Click().Invoke();

                case MouseButton.Right:
                    return Simulate.Events().Click().Invoke();

                case MouseButton.Middle:
                    return Simulate.Events().Click().Invoke();
            }

            return Task.CompletedTask;
        }
    }
}
