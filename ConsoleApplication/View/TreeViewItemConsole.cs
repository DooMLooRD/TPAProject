using System.Collections.ObjectModel;
using ViewModels.TreeViewItems;


namespace ConsoleApplication.View
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
