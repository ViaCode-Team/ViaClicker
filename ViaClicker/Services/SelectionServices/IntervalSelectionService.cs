namespace ViaClicker.Services.SelectionServices
{
    public class IntervalSelectionService(ConfigClicker config)
    {
        private readonly ConfigClicker _config = config;

        public int CurrentMs => _config.IntervalMs;

        public void SetMilliseconds(int intervalMs)
        {
            if (intervalMs <= 0)
                return;

            if (_config.IntervalMs == intervalMs)
                return;
            
            // Save mode
            if (intervalMs <= 99) intervalMs = 100;

            _config.IntervalMs = intervalMs;
            ClickerConfigService.Save(_config);
        }
    }
}
