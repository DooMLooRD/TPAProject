using System.Windows;
using ViewModels.DI.Base;
using ViewModels.Pages;

namespace WPFApplication.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = IoC.Get<MainWindowViewModel>();
        }
    }
}
