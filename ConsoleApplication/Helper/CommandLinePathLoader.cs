using System;
using System.IO;
using BusinessLogic.ViewModel;

namespace ConsoleApplication.Helper
{
    public class CommandLinePathLoader : IPathLoader
    {
        public string LoadPath()
        {

            string path = Console.ReadLine();

            if (path != null && File.Exists(path) && (path.Contains(".dll") || path.Contains(".xml")))
            {
                return path;
            }
            Console.WriteLine("Wrong path!");
            return null;

        }
    }
}
