namespace ViaClicker
{
    public class ConfigClicker
    {
        public int IntervalMs { get; set; }
        public MouseButton ClickButton { get; set; }
        public ConsoleKey StartKey { get; set; }
        public ConsoleKey PauseKey { get; set; }
    }

    public enum MouseButton
    {
        Left,
        Right,
        Middle
    }
}
