﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using ConsoleApplication.Converters;
using ConsoleApplication.View;
using ViewModels.DI.Base;
using ViewModels.Pages;
using ViewModels.TreeViewItems;

namespace ConsoleApplication
{
    class Program
    {
        #region IoC

        /// <summary>
        /// Inject dependencies
        /// </summary>
        private static void BindIoC()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            Compose(viewModel);
            IoC.Kernel.Bind<MainWindowViewModel>().ToConstant(viewModel);
            IoC.Kernel.Bind<string>().ToConstant("serialized.xml");

        }

        #endregion

        #region Mef


        public static void Compose(object obj)
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

        #endregion
       
        #region Properties
        public static MainWindowViewModel ViewModel { get; set; }
        public static TreeViewConsole ConsoleView { get; set; }
        #endregion

        #region Main

        static void Main(string[] args)
        {
            BindIoC();
            ViewModel = IoC.Get<MainWindowViewModel>();
            MainMenuView(String.Empty);
        }

        #endregion

        #region Views

        private static void TreeViewView(string message)
        {
            Console.Clear();
            Console.Write(message);
            Console.WriteLine("Path:" + ViewModel.PathVariable);
            PrintData();
            Console.WriteLine("Type id that you want to expand, if its already expanded shrink");
            Console.WriteLine("To serialize opened object and save to file type S/s/save and confirm action with 'Enter'");
            Console.WriteLine("Type 'Go back', 'b', 'B' if You want to go back to Menu");
            string temp = Console.ReadLine();
            switch (temp)
            {
                case "Go back":
                case "B":
                case "b":
                    {
                        MainMenuView(String.Empty);
                        break;
                    }
                case "S":
                case "s":
                case "Save":
                    {
                        ViewModel.SaveCommand.Execute(null);
                        TreeViewView("Object Serialized!");
                        break;
                    }
                default:
                    {
                        int parsedTemp;
                        if (!Int32.TryParse(temp, out parsedTemp) || parsedTemp < 0 || parsedTemp > ConsoleView.HierarchicalDataCollection.Count - 1)
                        {
                            TreeViewView("Incorrect format, try again\n");
                            return;
                        }
                        Expand(parsedTemp);
                        TreeViewView(String.Empty);
                        break;
                    }
            }
        }
        private static void MainMenuView(string message)
        {
            Console.Clear();
            Console.Write(message);
            Console.WriteLine("To open .dll or .exe type O/o/open and confirm action with 'Enter'\n" +
                              "To exit the program type E/e/exit and confirm with 'Enter'");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "O":
                case "o":
                case "Open":
                    {
                        Console.Clear();
                        Console.WriteLine("Type absolute Path of file you want to open");
                        ViewModel.HierarchicalAreas = new ObservableCollection<TreeViewItem>();
                        ViewModel.OpenCommand.Execute(null);
                        if (ViewModel.PathVariable == null)
                            MainMenuView("Wrong Path\n");
                        else
                        {
                            ConsoleView = new TreeViewConsole(new ObservableCollection<TreeViewItemConsole>(ViewModel.HierarchicalAreas.Select(n => new TreeViewItemConsole(n, 0))));
                            TreeViewView(String.Empty);
                        }
                        break;
                    }

                case "E":
                case "e":
                case "Exit":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        MainMenuView("Wrong Option!\n");
                        break;
                    }
            }

        }

        #endregion

        #region Methods

        private static void Expand(int index)
        {
            ConsoleView.Expand(index);
            Console.Clear();
            PrintData();
        }
        private static void PrintData()
        {
            int index = 0;
            foreach (TreeViewItemConsole itemConsole in ConsoleView.HierarchicalDataCollection)
            {
                string[] value = new string[4];
                value[0] = "id:" + index;
                value[1] = (string)ItemTypeEnumToStringConverter.Instance.Convert(itemConsole.TreeItem, null, null, null); ;
                value[2] = itemConsole.IsExpanded ? "[-] " : "[+] ";
                value[3] = itemConsole.TreeItem.ToString();
                PrintWithIndent(value, itemConsole.Indent);
                index++;
            }
        }


        private static void PrintWithIndent(string[] value, int indent)
        {
            Console.Write(new string(' ', indent * 3));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(value[0]);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(value[1]);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(value[2]);
            Console.ResetColor();
            Console.WriteLine(value[3]);
            //Console.WriteLine("{0}{1}{2}{3}{4}", new string(' ', indent * 2), value[0], value[1], value[2],value[3]);
        }

        #endregion

    }
}
