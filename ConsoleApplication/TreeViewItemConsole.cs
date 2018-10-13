using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ViewModel;

namespace ConsoleApplication
{
    public class TreeViewItemConsole
    {

        public TreeViewItem TreeItem { get; set; }
        public bool IsExpanded { get; set; }
        public int Indent { get; set; }

        public TreeViewItemConsole(TreeViewItem treeItem, int indent)
        {
            TreeItem = treeItem;
            IsExpanded = false;
            Indent = indent;
        }

        public ObservableCollection<TreeViewItem> Expand()
        {
            IsExpanded = true;
            TreeItem.IsExpanded = true;
            return TreeItem.Children;
        }
    }

}
