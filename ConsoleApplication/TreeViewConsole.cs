using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ViewModel;
using BusinessLogic.ViewModel.TreeViewItems;


namespace ConsoleApplication
{
    public class TreeViewConsole
    {
        public ObservableCollection<TreeViewItemConsole> HierarchicalDataCollection { get; set; }

        public TreeViewConsole(ObservableCollection<TreeViewItemConsole> hierarchicalDataCollection)
        {
            HierarchicalDataCollection = hierarchicalDataCollection;
        }

        public void Expand(int index)
        {
            TreeViewItemConsole item = HierarchicalDataCollection[index];
            if (!item.IsExpanded)
            {
                int i = 1;
                ObservableCollection<TreeViewItem> items = item.Expand();
                foreach (TreeViewItem treeViewItem in items)
                {
                    HierarchicalDataCollection.Insert(index + i, new TreeViewItemConsole(treeViewItem, item.Indent + 1));
                    i++;

                }

            }
            else
            {
                for (int i = item.TreeItem.Children.Count; i > 0; i--)
                {
                    if (HierarchicalDataCollection[index + i].IsExpanded)
                    {
                        Expand(index + i);
                        HierarchicalDataCollection.RemoveAt(index + i);
                    }
                    else
                    {
                        HierarchicalDataCollection.RemoveAt(index + i);
                    }

                }

                item.IsExpanded = false;
            }
        }


    }
}
