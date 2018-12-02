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
using ViewModels.DI.Base;
using ViewModels.Pages;


namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void On_Startup(object sender, StartupEventArgs e)
        {
            MainWindowViewModel viewModel=new MainWindowViewModel();
            Compose(viewModel);
            IoC.Kernel.Bind<MainWindowViewModel>().ToConstant(viewModel);
            IoC.Kernel.Bind<string>().ToConstant("serialized.xml");
        }

        public void Compose(object obj)
        {
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("plugins");
            string[] pluginsCatalogs = plugins.AllKeys;
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    directoryCatalogs.Add(new DirectoryCatalog(pluginsCatalog));
            }

            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(obj);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (Exception exception) when (exception is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException typeLoadException = (ReflectionTypeLoadException)exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach(ex => Console.WriteLine(ex.StackTrace));

                throw;
            }
        }


    }
}