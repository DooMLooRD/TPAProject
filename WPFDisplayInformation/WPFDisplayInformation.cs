using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewModels;

namespace WPFDisplayInformation
{
    [Export(typeof(IInformationDisplay))]
    public class WPFDisplayInformation : IInformationDisplay
    {
        public void ShowInfo(string information)
        {
            MessageBox.Show(information);
        }
    }
}
