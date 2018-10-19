using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ViewModel;

namespace ConsoleApplication.Helper
{
    public class CommandLinePathLoader : IPathLoader
    {
        public string LoadPath()
        {

            string path = Console.ReadLine();

            if (path != null && File.Exists(path) && path.Contains(".dll"))
            {
                return path;
            }
            Console.WriteLine("Wrong path!");
            return null;

        }
    }
}
