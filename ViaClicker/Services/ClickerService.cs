using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ViaClicker
{
    public class ClickerService
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        private CancellationTokenSource? cts;
        public bool IsRunning { get; private set; }

        // жёстко заданный интервал (мс)
        private const int IntervalMs = 100;

        public void Start()
        {
            if (IsRunning)
                return;

            cts = new CancellationTokenSource();
            IsRunning = true;
            Task.Run(() => Loop(cts.Token));
        }

        public void Stop()
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
                ClickLeft();
                await Task.Delay(IntervalMs, token);
            }
        }

        private void ClickLeft()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
        }
    }
}
