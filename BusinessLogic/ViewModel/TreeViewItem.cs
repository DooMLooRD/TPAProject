using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModel
{
    public class TreeViewItem
    {
        private bool wasBuilt;
        private bool isExpanded;
        public string Name { get; set; }
        public ItemTypeEnum ItemType { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }

        public ITreeViewItemBuilder Builder { get; set; }

        public TreeViewItem(string name, ItemTypeEnum itemType ,ITreeViewItemBuilder builder)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            this.wasBuilt = false;
            Name = name;
            ItemType = itemType;
            Builder = builder;
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                if (wasBuilt)
                    return;
                Children.Clear();
                Builder.BuildTreeView(Children);
                wasBuilt = true;
            }
        }

    }
}
