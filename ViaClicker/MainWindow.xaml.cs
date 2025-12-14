using System.Windows;
using System.Windows.Controls;
using ViaClicker.Services;
using ViaClicker.Services.SelectionServices;

namespace ViaClicker
{
    public partial class MainWindow : Window
    {
        private readonly ClickerService clicker;
        private readonly HotKeyService hotKeys;
        private readonly MouseButtonSelectionService mouseButtons;
        private readonly IntervalSelectionService intervalService;
        private readonly ConfigClicker config;

        public string IntervalText { get; set; } = "Interval";

        public MainWindow()
        {
            InitializeComponent();

            config = ClickerConfigService.Load();

            clicker = new ClickerService(config);
            hotKeys = new HotKeyService(clicker);
            mouseButtons = new MouseButtonSelectionService(config);
            intervalService = new IntervalSelectionService(config);

            SourceInitialized += OnSourceInitialized;
            Loaded += OnLoaded;
        }

        private void OnSourceInitialized(object? sender, EventArgs e)
        {
            hotKeys.Attach(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SetInitialRadioState();
            SetInitialInterval();
        }

        private void SetInitialInterval()
        {
            IntervalText = config.IntervalMs.ToString();
            DataContext = this;

            IntervalUnitComboBox.SelectedValue = "ms";
        }

        private void IntervalTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(IntervalTextBox.Text, out var value))
                return;

            var unit = IntervalUnitComboBox.SelectedValue as string;

            if (unit == "s")
                value *= 1000;

            intervalService.SetMilliseconds(value);
        }

        private void SetInitialRadioState()
        {
            var current = (int)config.ClickButton;

            foreach (var child in MouseButtonPanel.Children)
            {
                if (child is RadioButton rb &&
                    rb.Tag is string tag &&
                    int.TryParse(tag, out var value))
                {
                    rb.IsChecked = value == current;
                }
            }
        }

        private void MouseButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb &&
                rb.Tag is string tag &&
                int.TryParse(tag, out var value))
            {
                mouseButtons.Set((MouseSimLib.MouseButton)value);
            }
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
