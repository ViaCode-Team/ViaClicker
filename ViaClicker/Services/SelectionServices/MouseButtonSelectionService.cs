using MouseSimLib;

namespace ViaClicker.Services.SelectionServices
{
    public class MouseButtonSelectionService(ConfigClicker config)
    {
        private readonly ConfigClicker _config = config;

        public MouseButton Current => _config.ClickButton;

        public void Set(MouseButton button)
        {
            if (_config.ClickButton == button)
                return;

            _config.ClickButton = button;
            ClickerConfigService.Save(_config);
        }
    }
}
