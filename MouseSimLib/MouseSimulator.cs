using System.Runtime.InteropServices;
using MouseSimLib.Native;

namespace MouseSimLib
{
    public static class MouseSimulator
    {
        public static void Click(MouseButton button)
        {
            var flags = GetFlags(button);

            var inputs = new INPUT[]
            {
                Create(flags.down),
                Create(flags.up)
            };

            User32.SendInput(
                (uint)inputs.Length,
                inputs,
                Marshal.SizeOf<INPUT>()
            );
        }

        public static async Task ClickAsync(MouseButton button, int delayMs)
        {
            var flags = GetFlags(button);

            User32.SendInput(
                1,
                new[] { Create(flags.down) },
                Marshal.SizeOf<INPUT>()
            );

            await Task.Delay(delayMs);

            User32.SendInput(
                1,
                new[] { Create(flags.up) },
                Marshal.SizeOf<INPUT>()
            );
        }

        private static INPUT Create(uint flag)
        {
            return new INPUT
            {
                type = User32.INPUT_MOUSE,
                mi = new MOUSEINPUT
                {
                    dwFlags = flag
                }
            };
        }

        private static (uint down, uint up) GetFlags(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => (User32.MOUSEEVENTF_LEFTDOWN, User32.MOUSEEVENTF_LEFTUP),
                MouseButton.Right => (User32.MOUSEEVENTF_RIGHTDOWN, User32.MOUSEEVENTF_RIGHTUP),
                MouseButton.Middle => (User32.MOUSEEVENTF_MIDDLEDOWN, User32.MOUSEEVENTF_MIDDLEUP),
                _ => (0u, 0u)
            };
        }
    }
}
