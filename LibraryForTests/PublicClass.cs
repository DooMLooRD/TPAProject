using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class PublicClass : IPublicInterface
    {
        public int Property { get; set; }
        private AbstractClass abstractClass;

        public PublicClass()
        {

        }

        public PublicClass(int property)
        {
            Property = property;
        }

        public void MethodOne()
        {
            
        }
    }
}
