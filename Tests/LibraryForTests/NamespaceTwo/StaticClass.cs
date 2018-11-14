using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary.NamespaceTwo
{
    public static class StaticClass
    {
        private static int staticField;
        public static int ExtensionMethod(this int a)
        {
            return a;
        }

        public static string Method(string b)
        {
            return b;
        }

    }
}
