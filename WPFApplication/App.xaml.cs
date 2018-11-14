using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using BusinessLogic.DI.Base;
using BusinessLogic.Logging;
using BusinessLogic.Serialization;
using BusinessLogic.ViewModel;
using BusinessLogic.ViewModel.Pages;


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
            IoC.Kernel.Bind<string>().ToConstant("serialized.xml");

        }


    }
}
