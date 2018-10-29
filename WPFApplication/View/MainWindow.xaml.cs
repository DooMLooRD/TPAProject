using System.Windows;
using BusinessLogic.DI.Base;
using BusinessLogic.ViewModel.Pages;

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
