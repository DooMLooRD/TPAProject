using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ViewModel;

namespace ConsoleApplication
{
    class Program
    {
        public static MainWindowViewModel ViewModel { get; set; } = new MainWindowViewModel();
        public static TreeViewConsole ConsoleView { get; set; }

        static void Main(string[] args)
        {

            MainMenuView();
            ConsoleView = new TreeViewConsole(new ObservableCollection<TreeViewItemConsole>(ViewModel.HierarchicalAreas.Select(n => new TreeViewItemConsole(n, 0))));
            PrintData();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Path:"+ ViewModel.PathVariable);
                PrintData();
                Console.WriteLine("Type id that you want to expand, if its already expanded shrink");
                Expand(Int32.Parse(Console.ReadLine()));
            }

        }

        private static void MainMenuView()
        {
            Console.WriteLine("To open .dll or .exe type O/o/open and confirm action with 'Enter'\nTo exit the program type E/e/exit and confirm with 'Enter'");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "O":
                case "o":
                case "Open":
                    {

                        Console.WriteLine("Type absolute Path of file you want to open");
                        ViewModel.PathVariable = Console.ReadLine();
                        ViewModel.Click_Show.Execute(null);
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
                        Console.Clear();
                        Console.WriteLine("Wrong Option!");
                        MainMenuView();
                        break;
                    }
            }

        }

        public static void Expand(int index)
        {
            ConsoleView.Expand(index);
            Console.Clear();
            PrintData();
        }
        public static void PrintData()
        {
            int index = 0;
            foreach (TreeViewItemConsole itemConsole in ConsoleView.HierarchicalDataCollection)
            {
                string []value=new string[4];
                value[0] = "id:" + index;
                value[1] = "[" + itemConsole.TreeItem.ItemType + "]";
                value[2]= itemConsole.IsExpanded ? "[-] " : "[+] ";
                value[3] = itemConsole.TreeItem.Name;
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
    }
}
