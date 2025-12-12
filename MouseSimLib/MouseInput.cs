namespace MouseSimLib
{
    public static class Mouse
    {
        public static void Press(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    MouseInterop.Send(MouseInterop.MOUSEEVENTF_LEFTDOWN);
                    break;
                case MouseButton.Right:
                    MouseInterop.Send(MouseInterop.MOUSEEVENTF_RIGHTDOWN);
                    break;
                case MouseButton.Middle:
                    MouseInterop.Send(MouseInterop.MOUSEEVENTF_MIDDLEDOWN);
                    break;
            }
        }

        public static void Release(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    MouseInterop.Send(MouseInterop.MOUSEEVENTF_LEFTUP);
                    break;
                case MouseButton.Right:
                    MouseInterop.Send(MouseInterop.MOUSEEVENTF_RIGHTUP);
                    break;
                case MouseButton.Middle:
                    MouseInterop.Send(MouseInterop.MOUSEEVENTF_MIDDLEUP);
                    break;
            }
        }

        public static void Click(MouseButton button)
        {
            Press(button);
            Release(button);
        }

        public static void Move(int dx, int dy)
        {
            MouseInterop.Send(MouseInterop.MOUSEEVENTF_MOVE);
        }

        public static void Scroll(int delta)
        {
            MouseInterop.Send(MouseInterop.MOUSEEVENTF_WHEEL, (uint)delta);
        }

        public static async Task ClickAsync(MouseButton button, int delay = 50)
        {
            Press(button);
            await Task.Delay(delay);
            Release(button);
        }
    }
}