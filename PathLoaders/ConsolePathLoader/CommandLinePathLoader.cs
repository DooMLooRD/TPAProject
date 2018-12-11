using System;
using System.ComponentModel.Composition;
using System.IO;
using ViewModels;

namespace ConsolePathLoader
{
    [Export(typeof(IPathLoader))]
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
