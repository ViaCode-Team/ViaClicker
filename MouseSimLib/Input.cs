using System.Runtime.InteropServices;

namespace MouseSimLib.Native
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct INPUT
    {
        [FieldOffset(0)]
        public int type;

        [FieldOffset(8)]
        public MOUSEINPUT mi;
    }
}
