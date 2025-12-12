using System.Windows;

namespace ViaClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ClickerService _clicker;

        public MainWindow()
        {
            InitializeComponent();
            _clicker = new ClickerService();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _clicker.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _clicker.Stop();
        }
    }
}