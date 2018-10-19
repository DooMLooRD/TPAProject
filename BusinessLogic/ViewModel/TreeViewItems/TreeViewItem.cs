using System.Collections.ObjectModel;

namespace BusinessLogic.ViewModel.TreeViewItems
{
    public class TreeViewItem 
    {
        private bool _wasBuilt;
        private bool _isExpanded;
        public string Name { get; set; }
        public ItemTypeEnum ItemType { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }
        public ITreeViewItemBuilder Builder { get; set; }

        public TreeViewItem(string name, ItemTypeEnum itemType ,ITreeViewItemBuilder builder)
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
            this._wasBuilt = false;
            Name = name;
            ItemType = itemType;
            Builder = builder;
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                if (_wasBuilt)
                    return;
                Children.Clear();
                Builder.BuildTreeView(Children);
                _wasBuilt = true;
            }
        }
        

    }
}
