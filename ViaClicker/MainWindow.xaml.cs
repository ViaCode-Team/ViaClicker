using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ViaClicker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClickerConfigService.Load();
        }
    }
}
