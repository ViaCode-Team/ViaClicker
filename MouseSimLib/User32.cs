using System.Runtime.InteropServices;

namespace MouseSimLib.Native
{
    internal static class User32
    {
        internal const int INPUT_MOUSE = 0;

        internal const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        internal const uint MOUSEEVENTF_LEFTUP = 0x0004;
        internal const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        internal const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        internal const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        internal const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(
            uint nInputs,
            INPUT[] pInputs,
            int cbSize
        );
    }
}