using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary.NamespaceTwo
{
    public class ClassWithNestedStructure
    {
        private NestedStructure nestedStruct;
        public struct NestedStructure
        {
            private int one;
            private string two;
        }

    }
}
