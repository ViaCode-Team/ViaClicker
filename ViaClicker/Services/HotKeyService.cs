using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ViaClicker.Services
{
    public sealed class HotKeyService : IDisposable
    {
        private const int WM_HOTKEY = 0x0312;

        private const int START_ID = 1;
        private const int PAUSE_ID = 2;

        private HwndSource? source;
        private readonly ClickerService clicker;

        public HotKeyService(ClickerService clicker)
        {
            this.clicker = clicker;
        }

        public void Attach(Window window)
        {
            var handle = new WindowInteropHelper(window).Handle;
            source = HwndSource.FromHwnd(handle);
            source.AddHook(WndProc);

            var cfg = ClickerConfigService.Load();

            RegisterHotKey(handle, START_ID, 0, (uint)cfg.StartKey);
            RegisterHotKey(handle, PAUSE_ID, 0, (uint)cfg.PauseKey);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                var id = wParam.ToInt32();

                if (id == START_ID)
                    clicker.Start();

                if (id == PAUSE_ID)
                    clicker.Stop();

                handled = true;
            }

            return IntPtr.Zero;
        }

        public void Dispose()
        {
            if (source == null)
                return;

            var handle = source.Handle;
            UnregisterHotKey(handle, START_ID);
            UnregisterHotKey(handle, PAUSE_ID);
            source.RemoveHook(WndProc);
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
