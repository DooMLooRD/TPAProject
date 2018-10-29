using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BusinessLogic.DI.Base;
using BusinessLogic.Logging;
using BusinessLogic.ViewModel;
using WPFApplication.Helper;


namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void On_Startup(object sender, StartupEventArgs e)
        {
            IoC.Setup();
            IoC.Kernel.Bind<ILogFactory>().ToConstant(new BaseLoggerFactory());
            IoC.Kernel.Bind<IPathLoader>().ToConstant(new WPFPathLoader());
        }
    }
}
