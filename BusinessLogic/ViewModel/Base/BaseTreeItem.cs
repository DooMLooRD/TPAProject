using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModel
{
    public class BaseTreeViewModel
    {
        
        public string Name { get; set; }
        public BaseTreeViewModel(string name)
        {
            Name = name;
        }



    }
}
