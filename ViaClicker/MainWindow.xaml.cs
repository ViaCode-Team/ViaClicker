using System.Windows;
using ViaClicker.Services;

namespace ViaClicker
{
    public partial class MainWindow : Window
    {
        private readonly ClickerService clicker;
        private readonly HotKeyService hotKeys;

        public MainWindow()
        {
            InitializeComponent();

            var configCliker = ClickerConfigService.Load();

            clicker = new ClickerService(configCliker);
            hotKeys = new HotKeyService(clicker);

            SourceInitialized += OnSourceInitialized;
        }

        private void OnSourceInitialized(object? sender, EventArgs e)
        {
            hotKeys.Attach(this);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            clicker.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            clicker.Stop();
        }

        protected override void OnClosed(EventArgs e)
        {
            hotKeys.Dispose();
            base.OnClosed(e);
        }
    }
}
