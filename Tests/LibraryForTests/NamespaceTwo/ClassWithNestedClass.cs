using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary.NamespaceTwo
{
    public class ClassWithNestedClass
    {
        private NestedClass nestedClass;
        public class NestedClass
        {
            private int three;
            private string four;
        }
    }
}
